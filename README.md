# Redis-Client
create local redis settings json file

```json
{
  "server": {
    "enabled" :  true,
    "mode": "dev",
    "timeout": 65000,
    "prod": {
      "password": "P@ssw0rd",
      "clusters": [
        {
          "host": "server1.com",
          "port": 6379
        },
        {
          "host": "server2.com",
          "port": 6379
        },
        {
          "host": "server3.com",
          "port": 6379
        }
      ]
    },
    "dev": {
      "password": "P@ssw0rd",
      "clusters": [
        {
          "host": "server1.com",
          "port": 6379
        },
        {
          "host": "server2.com",
          "port": 6379
        },
        {
          "host": "server3.com",
          "port": 6379
        }
      ]
    }
  }
}
```
