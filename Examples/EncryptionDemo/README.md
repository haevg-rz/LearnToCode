# Encryption Demo

> Only for demo purpose, never use in production environment!

## Intro

> The .NET Framework provides implementations of many standard cryptographic algorithms. These algorithms are easy to use and have the safest possible default properties. In addition, the .NET Framework cryptography model of object inheritance, stream design, and configuration is extremely extensible.  
Source: [https://docs.microsoft.com/en-us/dotnet/standard/security/cryptography-model](https://docs.microsoft.com/en-us/dotnet/standard/security/cryptography-model)

## Guidance

As guidance we will use the document "[BSI TR-02102 Cryptographic Mechanisms](https://www.bsi.bund.de/EN/Publications/TechnicalGuidelines/tr02102/tr02102_node.html)" in version 2019-01
and some documents from the US National Institute of Standards and Technology NIST.

- [NIST SP 800-132 Recommendation for Password-Based Key Derivation: Part 1: Storage Applications](https://csrc.nist.gov/publications/detail/sp/800-132/final)

Additional there is a [Cryptographic Storage Cheat Sheet](https://owasp.org/www-project-cheat-sheets/cheatsheets/Cryptographic_Storage_Cheat_Sheet) from Open Web Application Security Project ([OWASP](https://owasp.org/)).

## Algorithm

> You can select an algorithm for different reasons: for example, for data integrity, for data privacy, or to generate a key. Symmetric and hash algorithms are intended for protecting data for either integrity reasons (protect from change) or privacy reasons (protect from viewing). Hash algorithms are used primarily for data integrity.

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

### Use Cases

You want to encrypt a file with a password, is this case you want to ensure data privacy with e.g. Aes, data integrity with e.g. HMACSHA512 and generating a key from a password with Rfc2898DeriveBytes.

You want to encrypt a file with a password, is this case you want to ensure data privacy with e.g. Aes, data integrity with e.g. HMACSHA512 and generating a key  with RNGCryptoServiceProvider in case of RSA oder derive a key in case of ECDH. The cipher text should be signed with RSA or ECDsa.

## Cryptographic primitives and Standards

[Cryptographic primitives](https://en.wikipedia.org/wiki/Cryptographic_primitive) are well-established, low-level cryptographic algorithms that are frequently used to build cryptographic protocols for computer security systems. These routines include, but are not limited to, one-way hash functions and encryption functions.

Cryptographic primitives, on their own, are quite limited. They cannot be considered, properly, to be a cryptographic system. For instance, a bare encryption algorithm will provide no authentication mechanism, nor any explicit message integrity checking. Only when combined in security protocols, can more than one security requirement be addressed. (See [Combining cryptographic primitives](https://en.wikipedia.org/wiki/Cryptographic_primitive#Combining_cryptographic_primitives))

For developer without any specialization in cryptosystems it is safer to use standards or cryptographic protocols which combine cryptographic primitives to ensure safty.

E.g. Cryptographic Message Syntax (CMS) for cryptographically protected messages or PGP, JWT, TLS, Signal Protocol.

[Any never ever invent your own crypto whatever!](https://security.stackexchange.com/questions/18197/why-shouldnt-we-roll-our-own) Use only trusted cryptography libraries!

## Features

Here are some features that you could implement.

- [Perfect Forward Secrecy (PFS)](https://en.wikipedia.org/wiki/Forward_secrecy)
- [Authenticated encryption](https://en.wikipedia.org/wiki/Authenticated_encryption)
- [Homomorphic encryption](https://en.wikipedia.org/wiki/Homomorphic_encryption)
- [Deniable Authentication](https://en.wikipedia.org/wiki/Deniable_authentication)
- [Deniable encryption](https://en.wikipedia.org/wiki/Deniable_encryption)
- [Malleability](https://en.wikipedia.org/wiki/Malleability_(cryptography))
- [Plausible deniability](https://en.wikipedia.org/wiki/Plausible_deniability)
- [Post-Quantum Cryptography - Quantum Resistance](https://en.wikipedia.org/wiki/Quantum_cryptography)
- [Symmetric key quantum resistance](https://en.wikipedia.org/wiki/Post-quantum_cryptography#Symmetric_key_quantum_resistance)
- [Crypto-agility](https://en.wikipedia.org/wiki/Crypto-agility)

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
