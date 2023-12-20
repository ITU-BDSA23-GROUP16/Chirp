---
title: _Chirp!_ Project Report
subtitle: ITU BDSA 2023 Group `16`
author:
  - "Christina Dieu <chdi@itu.dk>"
  - "Dima Jafar <dimj@itu.dk>"
  - "Jeppe Balslev <jebal@itu.dk>"
  - "Saynab Liibaan <saym@itu.dk>"
  - "Stanley Christensen <stwc@itu.dk>"
numbersections: true
---

# Design and Architecture of _Chirp!_

## Domain model

 


## Architecture — In the small

## Architecture of deployed application

## User activities

## Sequence of functionality/calls trough _Chirp!_

We have chosen to do a UML Sequence Diagram for every call to any endpoint in our version of Chirp!. The diagrams show the flow of data from the moment a user clicks around on the website, triggering GET or POST requests through Razor Pages, and that are then handled through our defined methods for so in our relevant repositories.
The endpoints we cover in the diagrams are as follows, for the following wishes of the user:

- to access Chirp!
- to authorize in some way
- to post a Cheep on public timeline
- to follow any arbitrary user
- to see the postings of followed users
- to see all registered users of Chirp!
- to delete one’s account.

![UML Sequence diagram showing an unauthorized user's call to root endpoint “/”.](./images/RootEndpoint.png)

![DESPACITO](./images/Onion-architecture.png)

# Process

## Build, test, release, and deployment

## Team work

## How to make _Chirp!_ work locally

## How to run test suite locally

# Ethics

## License

## LLMs, ChatGPT, CoPilot, and others

Although taken into consideration beforehand, we ended up not using any LLMs to assist us over the course of the project.
