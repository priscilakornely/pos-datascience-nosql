﻿using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Redinsgo
{
    public class Program
    {
        static void Main(string[] args)
        {
            var redis = ConnectionMultiplexer.Connect("localhost:6379");
            var db = redis.GetDatabase();

            var numeroUsuarios = 50;

            for (var r = 0; r < 10; r++)
            {
                db.SetAdd("cartelas", db.SetAdd("cartelas", Enumerable.Range(1, 99).Select(x => (RedisValue)x).ToArray()));

                for (var i = 1; i <= numeroUsuarios; i++)
                {
                    db.HashSet($"user:{i}", new HashEntry[]
                    {
                    new HashEntry("name", $"user{i}"),
                    new HashEntry("bcartela", $"cartela:{i}"),
                    new HashEntry("bscore", 0)
                    });

                    var numerosCartela = db.SetRandomMembers("cartelas", 15).Distinct().ToList();

                    while (numerosCartela.Count < 15)
                    {
                        var numero = db.SetRandomMember("cartelas");

                        if (!numerosCartela.Any(n => n == numero))
                            numerosCartela.Add(numero);
                    }

                    db.SetAdd($"cartela:{i}", numerosCartela.ToArray());
                }

                var venceu = false;
                var numerosSorteados = new List<short>();

                while (!venceu)
                {
                    var numeroSorteado = db.SetRandomMember("cartelas");

                    if (!numerosSorteados.Any(n => n == numeroSorteado))
                    {
                        numerosSorteados.Add((short)numeroSorteado);

                        for (var i = 1; i <= numeroUsuarios; i++)
                        {
                            var cartela = db.HashValues($"user:{i}")[1].ToString();

                            var possuiNumero = db.SetContains(cartela, numeroSorteado);

                            if (possuiNumero)
                            {
                                var total = db.HashIncrement($"user:{i}", "bscore", 1);

                                if (total == 15)
                                {
                                    venceu = true;
                                    Console.WriteLine($"user:{i} venceu!");
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
