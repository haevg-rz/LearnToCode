# Encryption Demo

> Only for demo purpose, never use in production environment!

## Intro

> The .NET Framework provides implementations of many standard cryptographic algorithms. These algorithms are easy to use and have the safest possible default properties. In addition, the .NET Framework cryptography model of object inheritance, stream design, and configuration is extremely extensible.  
Source: [https://docs.microsoft.com/en-us/dotnet/standard/security/cryptography-model](https://docs.microsoft.com/en-us/dotnet/standard/security/cryptography-model)

## Guidance

As guidance we will use the document "[BSI TR-02102 Cryptographic Mechanisms](https://www.bsi.bund.de/EN/Publications/TechnicalGuidelines/tr02102/tr02102_node.html)".

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

## Features

### Perfect Forward Secrecy (PFS)

> An encryption system has the property of forward secrecy if plain-text (decrypted) inspection of the data exchange that occurs during key agreement phase of session initiation does not reveal the key that was used to encrypt the remainder of the session.

Source: [https://en.wikipedia.org/wiki/Forward_secrecy](https://en.wikipedia.org/wiki/Forward_secrecy)

### Authenticated encryption

> Authenticated encryption (AE) and authenticated encryption with associated data (AEAD) are forms of encryption which simultaneously assure the confidentiality and authenticity of data.

Source: https://en.wikipedia.org/wiki/Authenticated_encryption

### Homomorphic encryption

> Homomorphic encryption is a form of encryption that allows computation on ciphertexts, generating an encrypted result which, when decrypted, matches the result of the operations as if they had been performed on the plaintext.

> Homomorphic encryption can be used for privacy-preserving outsourced storage and computation. This allows data to be encrypted and out-sourced to commercial cloud environments for processing, all while encrypted. In highly regulated industries, such as health care, homomorphic encryption can be used to enable new services by removing privacy barriers inhibiting data sharing. For example, predictive analytics in health care can be hard to apply due to medical data privacy concerns, but if the predictive analytics service provider can operate on encrypted data instead, these privacy concerns are diminished.

Source: https://en.wikipedia.org/wiki/Homomorphic_encryption

### Deniable Authentication

> In cryptography, deniable authentication refers to message authentication between a set of participants where the participants themselves can be confident in the authenticity of the messages, but it cannot be proved to a third party after the event.

Source: https://en.wikipedia.org/wiki/Deniable_authentication

### Deniable encryption

> In cryptography and steganography, plausibly deniable encryption describes encryption techniques where the existence of an encrypted file or message is deniable in the sense that an adversary cannot prove that the plaintext data exists

Source: https://en.wikipedia.org/wiki/Deniable_encryption

### Malleability 

> Malleability is a property of some cryptographic algorithms.[1] An encryption algorithm is "malleable" if it is possible to transform a ciphertext into another ciphertext which decrypts to a related plaintext. 

Source: https://en.wikipedia.org/wiki/Malleability_(cryptography)

### Plausible deniability

> In cryptography, deniable encryption may be used to describe steganographic techniques, where the very existence of an encrypted file or message is deniable in the sense that an adversary cannot prove that an encrypted message exists. In this case the system is said to be 'fully undetectable' (FUD)

Source: https://en.wikipedia.org/wiki/Plausible_deniability

### Post-Quantum Cryptography - Quantum Resistance

> Post quantum algorithms are also called "quantum resistant", because it is not known or provable that there will not be potential future quantum attacks against them.

> The need for post-quantum cryptography arises from the fact that many popular encryption and signature schemes (schemes based on **ECC and RSA**) can be broken using Shor's algorithm for factoring and computing discrete logarithms on a quantum computer.[...]

Source: [https://en.wikipedia.org/wiki/Quantum_cryptography](https://en.wikipedia.org/wiki/Quantum_cryptography)

#### Symmetric key quantum resistance

> Provided one uses sufficiently large key sizes, the symmetric key cryptographic systems like **AES** and SNOW 3G are already **resistant to attack by a quantum computer**. [...]  
> As a general rule, for 128 bits of security in a symmetric-key-based system, one can **safely use key sizes of 256 bits**. The best quantum attack against generic symmetric-key systems is an application of Grover's algorithm, which requires work proportional to the square root of the size of the key space. [...]

Source: [https://en.wikipedia.org/wiki/Post-quantum_cryptography#Symmetric_key_quantum_resistance](https://en.wikipedia.org/wiki/Post-quantum_cryptography#Symmetric_key_quantum_resistance)

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
