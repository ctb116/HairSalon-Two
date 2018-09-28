# _Hair Salon_

#### _Week 8 Independent Project for Epicodus, 9.28.2018_

#### By _**Catherine Bradley**_

## Description

_A Hair Salon webpage that stores stylist, specialty of stylist, and client information._

_Hair Salon allows users to add stylists, their specialty, as well as add clients belonging to a specific stylist. This program uses a many-to-many relationship between three tables: specialties, stylists, and clients._

### Specifications

* _1) User is greeted by a splash page with a links to view stylist, stylist specialty, or client information._
* _2) User can see all stylists, specialties, and clients._
* _3) User can add new Stylists with or without assigning that Stylist a speciality_
* _4) User can add a new Client and must assign that client to a Stylist._
* _5) User can add a new specialty at any time._

## Setup/Installation Requirements

* _Clone this repository_
* _Import bradley_catherine and bradley_catherine_tests databases to MySql_
* _Run program on localhost_

_To recreate databases in MySQL:_

* _> CREATE DATABASE bradley_catherine;_
* _> USE bradley_catherine;_
* _> CREATE TABLE stylists (id serial PRIMARY KEY, name VARCHAR(255));_
* _> CREATE TABLE clients (id serial PRIMARY KEY, name VARCHAR(255));_
* _> CREATE TABLE specialties (id serial PRIMARY KEY, type VARCHAR(255));_
* _> CREATE TABLE stylist_client (id serial PRIMARY KEY, client_id INT(32), stylist_id INT(32));_
* _> CREATE TABLE stylist_specialty (id serial PRIMARY KEY, specialty_id INT(32), stylist_id INT(32));_

_Test database is named bradley_catherine_tests with the same TABLE structure._

## Known Bugs

* _User can create a client and select a stylist but the stylist_client table does not update_
* _Users cannot edit the name of the stylist or any information of the clients _
* _Users cannot add a specialty and cannot give an existing stylist a specialty_

## Support and contact details

_For feedback, please contact Catherine Bradley at catherinetybradley@gmail.com_

## Technologies Used

* _C#_
* _MVC_

### Legal

*NA*
