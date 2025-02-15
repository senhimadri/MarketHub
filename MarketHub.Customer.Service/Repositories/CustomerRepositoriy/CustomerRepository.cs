﻿using MarketHub.CustomerService.Entities;
using MarketHub.CustomerService.Repositories.GenericRepository;
using MongoDB.Driver;

namespace MarketHub.CustomerService.Repositories.CustomerRepositoriy;

public class CustomerRepository(IMongoDatabase database)
                                : GenericRepository<Customer>(database, nameof(Customer)), ICustomerRepository;

