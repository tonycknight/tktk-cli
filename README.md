# tktk-cli

[![Build & Release](https://github.com/tonycknight/tktk-cli/actions/workflows/build.yml/badge.svg)](https://github.com/tonycknight/tktk-cli/actions/workflows/build.yml)

![Nuget](https://img.shields.io/nuget/v/tktk-cli)

A `dotnet tool` for various development tasks.

---

# Getting Started

## Dependenices

You'll need the [.Net 6 runtime](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) installed.

## Installation

``tktk-cli`` is available from [Nuget](https://www.nuget.org/packages/tktk-cli/):

```
dotnet tool install tktk-cli -g
```

---

# How to use

## Help:

```
tktk -?
```

## Generate Guids:

```
tktk guid -g 10
```

where `-g` is the number of guids you want to generate. Default is 5.

## Generate Passwords:

```
tktk pw -g 10 -l 32
```

Where:

`-g` is the number of passwords you want to generate, default is 5.

`-l` is the password length, default is 16.

## Decode JWT

```
tktk decodejwt <jwt>
```

Where:

`<jwt>` is the base-64 encoded JWT token to decode

---

