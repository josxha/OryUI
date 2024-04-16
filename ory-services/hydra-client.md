Let's create the OAuth 2.0 Client:
```bash
docker exec ory-hydra-1 hydra create client --endpoint http://127.0.0.1:4445/ --format json --grant-type client_credentials
```

Let's perform the client credentials grant:
```bash
docker exec ory-hydra-1 hydra perform client-credentials --endpoint http://127.0.0.1:4444/ --client-id "6cc3d412-55f5-45c1-980d-836f6bae0a7c" --client-secret "SNET7CauIJ5Zi~Th~0eeNA8dda"
```

Let's perform token introspection on that token. Make sure to copy the token you just got and not the dummy value.
```bash
docker exec ory-hydra-1 hydra introspect token --format json-pretty --endpoint http://127.0.0.1:4445/ ory_at_Wrb6h5D4NwQ3LbZ0NifZ1RdIFtD_V21RKXtmDnFAHcw.TZ0rK23CRu3CED13l_wZUF8Kz4GKZ6dtj8M55mfsDy0
```

Next, we will perform the OAuth 2.0 Authorization Code Grant. For that, we must first 
create a client that's capable of performing that grant:
```bash
docker exec ory-hydra-1 hydra create client --endpoint http://127.0.0.1:4445 --grant-type authorization_code,refresh_token --response-type code,id_token --format json --scope openid --scope offline --redirect-uri http://127.0.0.1:5555/callback
```

The following command starts a server that serves an example web application. The application will perform the OAuth 2.0 Authorization Code Flow using Ory Hydra. The web server runs on http://127.0.0.1:5555.
```bash
docker exec ory-hydra-1 hydra perform authorization-code --client-id 4ea5bc94-e443-4da1-bfd4-f1d739fce50e --client-secret uewqOuMdojDOMh70EZ6oR4PqIQ --endpoint http://127.0.0.1:4444/ --port 5555 --scope openid --scope offline
```