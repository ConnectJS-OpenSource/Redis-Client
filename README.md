# Redis-Client
create local redis settings json file as redis-conf.json

```json
  {
    "host": "localhost",
    "port": 6379,
    "password": "P@ssw0rd",
    "clusters": [{
      "host":"",
      "port":""
    }]
  }
```

Or Directly create the "RedisSettings" instance and pass in the constructor for RedisCache class
