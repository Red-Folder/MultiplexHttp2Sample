# Summary
The Http/2 protocol provides the ability to multiplex multiple requests over a single connection. This allows for more efficient use of connections - see https://http2.github.io/faq/#why-is-http2-multiplexed

I would expect to be able to use the .Net Core HttpClient to achieve this. My test (based on this repo) however indicate that there is a 1:1 ratio of request to TCP connections are made.

This repo is to support a [SO Question](https://stackoverflow.com/questions/47830834/can-you-achieve-http-2-multiplexing-with-net-core-httpclient)

# Work so far
Using the sample add, I generate two requests to www.google.com (which supports Http/2).

I get the following back from the app, so I know the requests are made over Http/2:

```
Response 1 - Http Version: 2.0, Http Status Code: OK
Response 2 - Http Version: 2.0, Http Status Code: OK
```

Monitoring the connections through Wireshark however, I can see that 2 TCP connections are generated.
