# Cimpress Access Managment (COAM) C# Sample

This repo is a very stripped down example of how to communicate with the Cimpress
Access Management system.

## Getting started

    git clone git@cimpress.githost.io:puma/api-management/coam-sample-csharp.git
    cd coam-sample-csharp

Edit `coam-sample-csharp/Program.cs` and input your client id and client secret,
which you can create at https://developer.cimpress.io/clients/create if you need one.

    msbuild
    coam_sample_csharp/bin/Debug/coam_sample_csharp.exe

This will print the following output:

```
Calling https://api.cimpress.io/auth/iam/v1/principals/{principal}/permissions/{resourceType}
[
  {
    "identifier": "*",
    "permissions": [
      "create:order",
      "debug:order",
      "read:order",
      "update",
      "update:order"
    ]
  }
]

Calling https://api.cimpress.io/auth/iam/v0/principals/{principal}/permissions/{resourceType}/{resourceIdentifier}
{
  "identifier": "vistaprint",
  "permissions": [
    "create:order",
    "debug:order",
    "read:order",
    "update",
    "update:order"
  ]
}
```

## What else should you be thinking about?

* Refreshing the token used to call COAM. They last for a while (currently 24 hours), but if this will be running on a server, they'll need to be refreshed when they get close to expiration. You can look at the 'exp' claim, which is
the UTC Unix timestamp of when the token expires.
* Caching the results of calling COAM. Currently COAM provides a Cache-Control max_age=30 (seconds) header, which
will save some round trips. We can increase that cache TTL if desired. If you know who your callers might be, you
could pre-warm the cache at application start and refresh at some interval
* The client secret used is a secret and should be treated like one. Don't check it into source control unless it
is encrypted. If you need guidance on how to securely store secrets, contact API Management
*
