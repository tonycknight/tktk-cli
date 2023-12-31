# tktk-cli

[![Build & Release](https://github.com/tonycknight/tktk-cli/actions/workflows/build.yml/badge.svg)](https://github.com/tonycknight/tktk-cli/actions/workflows/build.yml)

![Nuget](https://img.shields.io/nuget/v/tktk-cli)

A `dotnet tool` for various development tasks.

---

# Getting Started

## Dependenices

You'll need the [.Net 8 runtime](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) installed.

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

---

## Generate Guids

```
tktk guid -g 10
```

where `-g` is the number of guids you want to generate. Default is 5.

---

## Generate Passwords

```
tktk pw -g 10 -l 32
```

Where:

`-g` is the number of passwords you want to generate, default is 5.

`-l` is the password length, default is 16.

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

## Generate Waffle

Generate reams of tedious, bloated, pompous text, for fun & testing.

```
tktk waffle --paragraphs <paragraphs> --seed <random seed> --title --render <form>
```

Where:
* `<paragraphs>` is the number of paragraphs you want to generate. 1 by default.
* `<random seed>` is an optional integer to set the randomisation. Using the same integer will produce the same text on subsequent iterations.
* `<form>` is the render form, either text, html or markdown.

Include `-t` to add a title.

---

## Copyright and notices

(c) Tony Knight 2022.

Waffle - contains code originally produced & published by [Andrew Clarke](https://www.red-gate.com/simple-talk/author/andrew-clarke/) ([LinkedIn](https://www.linkedin.com/in/andrewclarke6/))
at [his article](https://www.red-gate.com/simple-talk/development/dotnet-development/the-waffle-generator/) and [public domain source code](https://www.red-gate.com/simple-talk/wp-content/uploads/imported/465-WaffleEngine.cs.txt).

