# tkdev-cli

[![Build & Release](https://github.com/tonycknight/tkdev-cli/actions/workflows/build.yml/badge.svg)](https://github.com/tonycknight/tkdev-cli/actions/workflows/build.yml)

![Nuget](https://img.shields.io/nuget/v/tkdev-cli)

A `dotnet tool` for various development tasks.

---

# Getting Started

## Dependenices

You'll need the [.Net 6 runtime](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) installed on your Windows machine.

## Installation

``tkdev-cli`` is available from [Nuget](https://www.nuget.org/packages/tkdev-cli/):

```
dotnet tool install tkdev-cli -g
```

---

# How to use

## Help:

```
tkdev -?
```

## Generate Guids:

```
tkdev guid -g 10
```

where `-g` is the number of guids you want to generate. Default is 5.

## Generate Passwords:

```
tkdev pw -g 10 -l 32
```

Where:

`-g` is the number of passwords you want to generate, default is 5.

`-l` is the password length, default is 16.

---
