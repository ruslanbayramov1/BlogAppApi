﻿using BlogApp.BL.Exceptions.Common;
using BlogApp.BL.ExternalServices.Interfaces;
using BlogApp.Core.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace BlogApp.BL.ExternalServices.Implements;

public class RedisService : ICacheService
{
    private readonly IDistributedCache _db;
    public RedisService(IDistributedCache db)
    {
        _db = db;
    }

    public async Task<T?> Get<T>(string key)
    {
        string? jsonData = await _db.GetStringAsync(key);
        if (jsonData == null) throw new NotFoundException<User>("Item not found!");
        return JsonSerializer.Deserialize<T>(jsonData);
    }

    public async Task Set<T>(string key, T data, int seconds = 300)
    {
        string jsonData = JsonSerializer.Serialize(data);
        var opt = new DistributedCacheEntryOptions();
        opt.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(seconds);
        await _db.SetStringAsync(key, jsonData, opt);
    }
}