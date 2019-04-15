# Episerver Find - Crawler on client side #

This repository is the an example for using the Episerver Find Client APIs reference implementation for crawling purpose.

## What is the idea? ##

* Use EPiServer Find indices.
* Execute crawling on the Episerver site/console app.
* Customize what content/information on target site we (developers) want to crawl.
* Crawled content can be searched on client site.

## Use-cases ##

* Support seaching content from child website on parent site.
* Support seaching related content on parent site.
* Verify our EPiServer Find indices.

## Reference documents ##

* .NET-Client-API - Client class: https://world.episerver.com/documentation/developer-guides/find/NET-Client-API/Client-class/
* .NET-Client-API - Indexing: https://world.episerver.com/documentation/developer-guides/find/NET-Client-API/Indexing/

## Development note ##

* AngleSharp: a .NET library that gives you the ability to parse angle bracket based hyper-texts like HTML, SVG, and MathML.  https://github.com/AngleSharp/AngleSharp
* Log4net: a tool to help the programmer output log statements to a variety of output targets. https://github.com/apache/logging-log4net
* This project is using EPiServer.Find 13.0.5. https://nuget.episerver.com/package/?id=EPiServer.Find