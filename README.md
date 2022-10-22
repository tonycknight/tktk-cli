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

## About:

```
tktk about
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
tktk jwt <jwt>
```

Where:

`<jwt>` is the base-64 encoded JWT token to decode

---

## Base conversions

```
tktk conv <value>
```

Where: 

`<value>` is a decimal, hexadecimal or binary value. Decimal and binary values always yield their hexadecimal countarpart, hexadecimal values yield their decimal counterpart.

e.g. Decimal:

```
tktk conv 1234
```

Hexadecimal:

```
tktk conv 0x0a0b
```

Binary:

```
tktk conv 0b10101
```

---

## Epoch

Convert integer or date time values to their Unix epoch equivalents, or `now` for the curent time.

```
tktk epoch <value>
```

Where `<value>` is an integer, local date time or `now`

E.g.

`tktk epoch 0`

`tktk epoch 2020-01-01T01:30:00`

`tktk epoch now`

---

## Copyright and notices

(c) Tony Knight 2022.

Waffle - contains code originally published by [Andrew Clarke](https://www.red-gate.com/simple-talk/author/andrew-clarke/)
See [his article](https://www.red-gate.com/simple-talk/development/dotnet-development/the-waffle-generator/) and [source code](https://www.red-gate.com/simple-talk/wp-content/uploads/imported/465-WaffleEngine.cs.txt)

