# MCP Identity and Access Managment (IAM) C# Sample

This repo is a very stripped down example of how to communicate with the Cimpress
MCP IAM service.

## Getting started

    git clone https://mcpstash.cimpress.net/scm/ce/iam-sample-csharp.git
    cd iam-sample-csharp

Edit `iam-sample-csharp/Program.cs` and input your client id and client secret,
which API Management can setup for you.

    msbuild
    iam_sample_csharp/bin/Debug/iam_sample_csharp.exe

This will print the following output:

```
Calling https://development.api.cimpress.io/iam/v0/user-permissions/{sub}/{resourceType}
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

Calling https://development.api.cimpress.io/iam/v0/user-permissions/{sub}/{resourceType}/{resourceIdentifier}
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

* Refreshing the token used to call IAM. They last for a while (currently 24 hours), but if this will be running on a server, they'll need to be refreshed when they get close to expiration. You can look at the 'exp' claim, which is
the UTC Unix timestamp of when the token expires.
* Caching the results of calling IAM. Currently IAM provides a Cache-Control max_age=30 (seconds) header, which
will save some round trips. We can increase that cache TTL if desired. If you know who your callers might be, you
could pre-warm the cache at application start and refresh at some interval
* The client secret used is a secret and should be treated like one. Don't check it into source control unless it
is encrypted. If you need guidance on how to securely store secrets, contact API Management
*
