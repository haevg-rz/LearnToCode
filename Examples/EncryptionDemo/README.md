# Encryption Demo

> Only for demo purpose, never use in production environment!

## Intro

The .NET Framework provides implementations of many standard cryptographic algorithms. These algorithms are easy to use and have the safest possible default properties. In addition, the .NET Framework cryptography model of object inheritance, stream design, and configuration is extremely extensible.  
Source: [https://docs.microsoft.com/en-us/dotnet/standard/security/cryptography-model](https://docs.microsoft.com/en-us/dotnet/standard/security/cryptography-model)

## Guidance

As guidance we will use the document "[BSI TR-02102 Cryptographic Mechanisms](https://www.bsi.bund.de/EN/Publications/TechnicalGuidelines/tr02102/tr02102_node.html)".

## Algorithm

You can select an algorithm for different reasons: for example, for data integrity, for data privacy, or to generate a key. Symmetric and hash algorithms are intended for protecting data for either integrity reasons (protect from change) or privacy reasons (protect from viewing). Hash algorithms are used primarily for data integrity.

- Data privacy:
  - Aes
- Data integrity:
  - HMACSHA256
  - HMACSHA512
- Digital signature:
  - ECDsa
  - RSA
- Key exchange:
  - ECDiffieHellman
  - RSA
- Random number generation:
  - RNGCryptoServiceProvider
- Generating a key from a password:
  - Rfc2898DeriveBytes

Source: [https://docs.microsoft.com/en-us/dotnet/standard/security/cryptography-model#choosing-an-algorithm](https://docs.microsoft.com/en-us/dotnet/standard/security/cryptography-model#choosing-an-algorithm)

## Usage of cryptographic primitive

### Hash

### RSA

### ECC

### Combination of cryptographic primitive

#### Symmetric encryption with a password

#### Asymmetric encryption with an elliptic curve

## Usage of standardized syntax for the exchange of encrypted data

### JavaScript Object Signing and Encryption (JOSE)

### Cryptographic Message Syntax (CMS, PKCS#7)
