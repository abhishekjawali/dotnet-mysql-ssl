#Sample code for establishing SSL connection to Aurora MySQL DB from .Net core

## Certificate setup
Windows requires the certificates to be trusted in its trust store.

Download the certificate bundle in PKCS7 format from https://s3.amazonaws.com/rds-downloads/rds-combined-ca-bundle.p7b

Install the CA root certificate into Windows trust store

## Running the code
1. Update the DB credentials in App.config
2. Execute
    ```
    dotnet run
    ```