# Okta Identity Engine Dynamic Authentication Control
## DRAFT
This document is a work in progress.

## Problem
The functionality of the current Okta Identity Engine SDK for .NET ****REQUIRES**** policy configuration that results in a finite set of known JSON response data structures in a predetermined but undocumented sequence.  If a feature or functionality is added or changed, or if policy configuration is modified, the resulting response structures **MAY** not be supported by the SDK resulting in unexpected [sample application](https://github.com/okta/okta-idx-dotnet/tree/master/samples/samples-aspnet/embedded-auth-with-sdk) behavior or unexpected [Idx Client](https://github.com/okta/okta-idx-dotnet/blob/master/src/Okta.Idx.Sdk/IdxClient.cs) behavior, including unhandled exceptions. 

## Abstract
This document describes **Okta Identity Engine** response structure and a strategy for rendering responses to accept input for the purpose of authentication.  Information herein provides the basis for the `Dynamic Authentication Control`.  To get started quickly, skip to [Integrations](#integrations).

## Introduction
**Okta Identity Engine** hereinafter referred to as **OIE**, is an authentication API modeled as a [state machine](https://developer.mozilla.org/en-US/docs/Glossary/State_machine); this allows a consumer of the API to design a rendering loop that renders responses from **OIE** that is resilient to changes in [Sign-on policies](https://help.okta.com/en/prod/Content/Topics/Security/policies/policies-home.htm).  Code in this repository is a reference implementation referred to as the **Okta Dynamic Authentication Control** herinafter referred to as **ODAC**.  

## Terminology
This document uses common industry terminology as well as **OIE** specific terminology unique to this document and related works.

### Ion Spec Terminology
This document makes use of terminology defined in the ion spec to reference components of an **OIE** response, see section [1.1 Terminology](https://ionspec.org/#_terminology) of the Ion spec.  Common terminology used in this document follows:

- **Member** - A JSON name/value pair as defined in [RFC 7159](https://datatracker.ietf.org/doc/html/rfc7159#section-4)
- **Root Object** - The single JSON object at the root of an Ion content structure.
- **Value Object** - A JSON object with a member named `value`.
- **Collection Object** - A *Value Object* where the value member is a JSON array.  If a JSON value is an element in a *Collection Object's* value array, it is said that the Collection Object *contains* that value.

### Mv* Pattern Terminology
This document makes use of terminology commonly associated with *Model-View-* patterns such as [MVC](https://en.wikipedia.org/wiki/Model%E2%80%93view%E2%80%93controller) and [Mvvm](https://en.wikipedia.org/wiki/Model%E2%80%93view%E2%80%93viewmodel).

- **Model** - A domain model that defines the structure of data for an application.
- **View** - The structure layout and appearance of what a user sees on the screen.  It displays a representation of the model.
- **ViewModel** - Loosely defined in this document as the relationship between the model and associated actions that a user may take to affect it.
- **Control** - A UI element that enables interaction or displays content.

### Okta Identity Engine Terminology
This section describes **OIE** specific terminology.

- **Remediation** - A high level concept in **OIE**.  Remediation can be thought of as an action one may take to *"remedy or 'set right' the unauthenticated state of the user"*.
- **Remediation collection** - The `remediation` [member](#ion-spec-terminology) of the [root object](#ion-spec-terminology) of an **OIE** response; it is an ion [collection object](#ion-spec-terminology).  See also, [OIE Response](#oie-response).
- **Remediation invocation** - An HTTP call made to an endpoint associated with a remediation.  See also, [Remediation Object](#remediation-object).
- **Remediation object** - A [value object](#ion-spec-terminology) contained in the `remediation` collection member of an **OIE** response.  Remediation objects provide the necessary information to make a `remediation invocation`.  See also, [Remediation Object](#remediation-object).
- **Remediation parameter** - A JSON object expected as an argument to a `remediation invocation`.  The structure of a `remediation parameter` is described by the dependent `remediation object`.  See also, [Remediation Object](#remediation-object).
- **Remediation parameter property** - A JSON object that describes a property of a `remediation parameter`, it may or may not be a [value object](#ion-spec-terminology).  See also, [Remediation Parameter](#remediation-parameter).
- **Authenticator** - A way to authenticate a user.
- **Authenticator enrollment** - An `authenticator` that a user has registered or "enrolled" as a way to authenticate.
- **State handle** - A value that a developer may use to reference a specific authentication session.

## OIE Response
An **OIE** response is the [root object](#ion-spec-terminology) and the [members](#ion-spec-terminology) it contains.  The following shows the initial [members](#ion-spec-terminology) of an **OIE Response**.

### Root Object
```json
{
    "version": "1.0.0",
    "stateHandle": "028Iw6C9XIrN1Pez_NZ6ulTaQmgBVr6R7WFxWr6E6E",
    "expiresAt": "2022-03-03T15:31:22.000Z",
    "intent": "LOGIN",
    "remediation": {
        "type": "array",
        "value": [
            ... see Remediation Object section
        ]
    },
    "currentAuthenticator": {
        "type": "object",
        "value": {
            ... see Authenticator Object section
        }
    },
    "cancel": {
        "rel": [
            "create-form"
        ],
        "name": "cancel",
        "href": "https://dotnet-sdk-idx.okta.com/idp/idx/cancel",
        "method": "POST",
        "produces": "application/ion+json; okta-version=1.0.0",
        "value": [
            {
                "name": "stateHandle",
                "required": true,
                "value": "028Iw6C9XIrN1Pez_NZ6ulTaQmgBVr6R7WFxWr6E6E",
                "visible": false,
                "mutable": false
            }
        ],
        "accepts": "application/json; okta-version=1.0.0"        
    }
}
```
The members included in the OIE response [root object](#ion-spec-terminology) change as the authentication flow progresses.  The primary member of concern for the purposes of authentication is `remediation`.  See also, [Remdediation Object](#remediation-object) and [Sdk Object Model](#sdk-object-model).

The [root object](#ion-spec-terminology) may contain the following members at different points during the authentciation flow:

- **remediation** - The `remediation` member is an ion [collection object](#ion-spec-terminology) that contains `remediation objects`.  See also [Remediation Object](#remediation-object).
- **currentAuthenticator** - The `currentAuthenticator` member provides additional information for the authenticator in the current context of the authentication session.
- **authenticators** - The `authenticators` member is an ion [collection object](#ion-spec-terminology) that contains objects that describe []().
- **authenticatorEnrollments** - The `authenticatorEnrollments` member is an ion [collection object](#ion-spec-terminology) that contains objects that describe [authenticator enrollments](#okta-identity-engine-terminology).

### Remediation Object
A `remediation object` is an ion [collection object](#ion-spec-terminology) that contains [remediation parameter properties](#okta-identity-engine-terminology).  
```json
{
    "rel": [
        "create-form"
    ],
    "name": "identify",
    "href": "https://dotnet-sdk-idx.okta.com/idp/idx/identify",
    "method": "POST",
    "produces": "application/ion+json; okta-version=1.0.0",
    "value": [
        {
            "name": "identifier",
            "label": "Username",
            "required": true
        },
        {
            "name": "credentials",
            "type": "object",
            "form": {
                "value": [
                    {
                        "name": "passcode",
                        "label": "Password",
                        "secret": true
                    }
                ]
            },
            "required": true            
        },
        {
            "name": "rememberMe",
            "type": "boolean",
            "label": "Remember this device"
        },
        {
            "name": "stateHandle",
            "required": true,
            "value": "02rEajQcd651m1YehmCEOjfBkcpOBcEIJ2PZO6sSqJ",
            "visible": false,
            "mutable": false
        }
    ],
    "accepts": "application/json; okta-version=1.0.0"
}
```

### Remediation Parameter
To invoke the associated [remediation invocation](#okta-identity-engine-terminology) the JSON argument provided in the invocation request body must have [members](#ion-spec-terminology) as described by the [remediation parameter property](#okta-identity-engine-terminology) contained in the `remediation object`.  The following JSON may be used as an argument to the `remediation object` [previously defined](#remediation-object).  See also, [Sdk Object Model](#sdk-object-model):
```json
{
    "identifier": "username@domain.com",
    "credentials": {
        "passcode": "Password1234"
    },
    "rememberMe": false,
    "stateHandle": "02rEajQcd651m1YehmCEOjfBkcpOBcEIJ2PZO6sSqJ"
}
```

## Dynamic Authentication Control
This section describes the building blocks that compose the Dynamic Authentication Control.  In the majority of cases it is not necessary to interact directly with the components described here, this information is provided in the interest of completeness.

Because members included in the OIE response [root object](#ion-spec-terminology) change as the authentication flow progresses, it isn't possible to define a static [class](https://en.wikipedia.org/wiki/Class_(computer_programming)) that represents it.  In order to reference the members of the OIE response [root object](#ion-spec-terminology) an Ion API is recommended.  See also, [Ion Object Model](#ion-object-model).

### Ion Object Model
This section describes the API used to interact with the ion json responses received from OIE.  In the majority of cases it will not be necessary to interact directly with the raw OIE responses, this information is provided in the interest of completeness.

Ion is used to describe the class or object structure of higher level concepts; it does not inherently provide strongly typed classes that describe a problem domain.  Instead, it provides an intermediate language, defined as a superset to json, that defines the class or object structure within a problem domain.  To simplify interaction with Ion based responses, an Ion based API library is recommended.  The reference Ion parsing implemention is found here https://github.com/BryanApellanes/Bam.Ion/tree/bam-ion-v0.2. 


- **IonValueObject** - Represents an Ion value object, see https://ionspec.org/#valueobjects.
- **IonMember** - Represents a property of an ion value object.

The following shows how to parse an ion json response as an IonValueObject.

```csharp
IonValueObject obj = IonValueObject.ReadObject(ionJson);
```

The IonMember class is used to cast or convert as necessary a property value to the appropriate type.

```csharp
IonValueObject obj = IonValueObject.ReadObject(ionJson);
IonMember member = obj["href"];
string href = member.ValueAs<string>();
```

### Dynamic Authentication Control Object Model
This section describes the API used to interact with the objects that result from the parsed ion json responses received from OIE.

The SDK Object model uses the Ion Object Model internally to provide high level class constructs that describe the **OIE** problem domain.

- **IdentityState** - A class definition that ecapsulates the [Root Object](#root-object) of an [OIE Response](#oie-response).
- **Remediation** - A class definition that encapsulates a [Remediation](#okta-identity-engine-terminology).

### View Render Loop
An [OIE Response](#oie-response) can be thought of as a [ViewModel](https://viewmodel.org/) and as such all related concepts apply.  The simplest rendering strategy for an OIE Response is to render each remediation included in `IdenityState.Remediations`. See example [okta-dotnet-identityengine](https://github.com/okta/okta-dotnet-identityengine/blob/release-v0.1/Samples/Okta.IdentityEngine.Mvc.Sample/Views/Authentication/SignIn.cshtml#L14). 

Each _Remediation Object_ included in `IdentityState.Remediations` describes a potential _Remediation Invocation_ which results in a change in the IdentityState.

![Remediation Render Loop](https://github.com/okta/okta-dotnet-identityengine/blob/release-v0.1/img/OIE-Render-Loop.png)

### Successful Authentication
After a _Remediation Invocation_ the [OIE Response](#oie-response) may have a member named `successWithInteractionCode` which is both an [Ion Link](https://ionspec.org/#links) and an [Ion Value Object](#ion-object-model) which provides the necessary information used to acquire an authentication token.  If the `successWithInteractionCode` member is present, authentication has succeeded and you may proceed to retrieve an authentication token using the information provided.

```json
{
   "successWithInteractionCode":{
      "rel":[
         "create-form"
      ],
      "name":"issue",
      "href":"https://foo.com/oauth2/foo/v1/token",
      "method":"POST",
      "value":[
         {
            "name":"grant_type",
            "required":true,
            "value":"interaction_code"
         },
         {
            "name":"interaction_code",
            "required":true,
            "value":"Mj36FIck-Fr1845qXozlmCVD64Mx3sk3DrvPNVFB2E4"
         },
         {
            "name":"client_id",
            "required":true,
            "value":"foo"
         },
         {
            "name":"client_secret",
            "required":true
         },
         {
            "name":"code_verifier",
            "required":true
         }
      ],
      "accepts":"application/x-www-form-urlencoded"
   }
}
```

## Integration
There are three levels of integration the `Dynamic Authentication Control` supports; each provides increasing levels of customization capabilities.

- Minimum code - Include the appropriate `IOktaPlatform` implementation in your project and call GetSignInViewAsync().
- Custom UI - Provide custom UI for view model data elements.
- Custom Logic - Provide custom code logic for phases of the pipeline execution that matter to you.

### Components
The primary components responsible for exposing OIE functionality are as follows:

![Components](../release-v0.1/img/Components.png)

### Pipeline Execution

![Pipeline Execution](../release-v0.1/img/Sequence.png)
