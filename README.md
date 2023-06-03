Elasticsearch implemented in ASP.NET Core Web API for learning purposes. WIP.

# Install Elasticsearch

There are many ways to do this, I decided to [install Elasticsearch with Docker](https://www.elastic.co/guide/en/elasticsearch/reference/current/docker.html) and run a single-node cluster for testing purposes.
I use Windows with [Rancher Desktop](https://rancherdesktop.io/) and WSL2.


## Adjust vm.max_map_count

By default, many systems set _vm.max_map_count_ to _65530_, which is often too low for Elasticsearch. It's recommended to set _vm.max_map_count_ to at least _262144_ for Elasticsearch to function properly.

- Open a WSL2 terminal
- Increase the _vm.max_map_count_: ```sysctl -w vm.max_map_count=262144```
- Verify it was set correctly: ```sysctl vm.max_map_count```

## Pull image and start container

- Pull image: ```docker pull docker.elastic.co/elasticsearch/elasticsearch:8.8.0```
- Create Docker network: ```docker network create elastic```
- Start container: ```docker run --name es01 -m 4GB -p 9200:9200 -it docker.elastic.co/elasticsearch/elasticsearch:8.8.0```

# Add configuration in .NET app

After running the above commands, Elasticsearch service is accessible on port 9200 on the host machine (https://localhost:9200).
A user with the _username_ __elastic__ will be created automatically and the _password_ will be output to the console on start.
A HTTP CA certificate SHA-256 _fingerprint_ will also be output console on start.

```
"ElasticsearchOptions": {
  "Url": "Environment-specific",
  "FingerPrint": "Environment-specific/secret",
  "Username": "Environment-specific/secret",
  "Password": "Environment-specific/secret"
}
```
