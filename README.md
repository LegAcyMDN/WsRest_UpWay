# WsRest_Upway
Backend for SAE4A.01

## Features
- [x] In-memory caching
- [x] Redis caching
- [x] Rate-limiter
- [x] Docker build script

## Configuration
### DB_CONNECTION_URL
Set the url to connect to the database. <br>
Ex: `Server=localhost;port=5432;Database=upway;uid=postgres;password=upways;SearchPath=upways`

### JWT_SECRET_KEY
Secret key to encrypt the JWT. A random key can be generated with `openssl rand -base64 32`

### JWT_ISSUER
URL of the backend endpoint. <br>
Ex: `https://api.upway.com`

### JWT_AUDIENCE
URL of the frontend endpoint. <br>
Ex: `https://upway.com`

### CACHE_SIZE
Maximum size the cache can use (in bytes). <br>
Ex: `10000`

### CACHE_SLIDING_EXPIRATION
How long can a resource be kept in cache without being requested. <br>
Ex: `5m`

### CACHE_ABSOLUTE_EXPIRATION
How long can a resource be kept in cache regardless of if it is being requested. <br>
Ex: `10m`

### FRONTEND_URL
List of URLs to allow in CORS origin, seprated by `;` <br>
Ex: `https://upway.com;https://upway.fr`

### BRAINTREE_ENV
Set the environement for the payment system, must be either `sandbox` or `production`.

### BRAINTREE_MERCHANT_ID
Set the merchant id for the payment system.

### BRAINTREE_PUBLIC_KEY
Set the public key for the payment system.

### BRAINTREE_PRIVATE_KEY
Set the private key for the payment system