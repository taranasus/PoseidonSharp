﻿using System;
using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using PoseidonSharp;

namespace PoseidonConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test case 1 for signed message of get api key url
            //Calculate sha256 specific to loopring of url
            BigInteger testOneInput = SHA256Helper.CalculateSHA256HashNumber("GET&https%3A%2F%2Fuat3.loopring.io%2Fapi%2Fv3%2FapiKey&accountId%3D11087");
            Debug.Assert(testOneInput == BigInteger.Parse("19400808358061590369279192378878962429412529891699423035130831734199348072763"), "Hash doesn't match expected hash!");
             Console.WriteLine($"Hash of test one is {testOneInput}");
            Eddsa eddsa = new Eddsa(BigInteger.Parse("15262223708097584402615283257936266522564860189809682357548133077263290491192"), Environment.GetEnvironmentVariable("LoopringPrivateKey", EnvironmentVariableTarget.User)); //Put in the calculated poseidon hash in order to Sign
            string signedMessage = eddsa.Sign();
            Debug.Assert(signedMessage == "0x02cd51ee31c9d63e6d9796704249fbccaba8fd287e4c7d412bc4d6d88801bb0a067de03f99a1a1194a098522e686a1940024946535d45cbbd02b3bb38722d9f02fa6e5be861a24168738837e7b7f38e4379b26a54a60673afde303e75f47b769", "Signed message doesn't match expected signed message");
            Console.WriteLine($"Signed message: {signedMessage}");

            
            //Test case 2
            int MAX_INPUT_TWO = 4; //Max Input should be the number of BigInteger inputs
            Poseidon poseidonTwo = new Poseidon(MAX_INPUT_TWO + 1, 6, 53, "poseidon", 5, _securityTarget: 128);
            BigInteger[] inputsTwo = { BigInteger.Parse("1233333333333333"), BigInteger.Parse("9400000000000000000000000000"), BigInteger.Parse("1223123"), BigInteger.Parse("544343434343434343") };
            BigInteger testTwoPoseidonHash = poseidonTwo.CalculatePoseidonHash(inputsTwo);
            Debug.Assert(testTwoPoseidonHash == BigInteger.Parse("3642840179269730552612336878249257609263354431767353053799083195998559566113"), "Hash doesn't match expected hash!");
            Console.WriteLine($"Hash of test two is {testTwoPoseidonHash}");
            Eddsa eddsaTwo = new Eddsa(testTwoPoseidonHash, Environment.GetEnvironmentVariable("LoopringPrivateKey", EnvironmentVariableTarget.User)); //Put in the calculated poseidon hash in order to Sign
            string signedMessageTwo = eddsaTwo.Sign();
            Debug.Assert(signedMessageTwo == "0x0b60e3d275b059b7a7f485e8182b32de7d842090b828e0471aad2fee4ad1f58c246cb6d8b538fe9929993b44a86ea90f50bdd346db600c193e1a8c62340a6d871f5aa69ca257feea363ab9b55ca52372f1fcd404964f27c3bae07e5d8f46d53a", "Signed message doesn't match expected signed message");
            Console.WriteLine($"Signed message: {signedMessageTwo}");

            //Test case 3
            int MAX_INPUT_THREE = 13; //Max Input should be the number of BigInteger inputs
            Poseidon poseidonThree = new Poseidon(MAX_INPUT_THREE + 1, 6, 53, "poseidon", 5, _securityTarget: 128);
            BigInteger[] inputsThree = { 
                BigInteger.Parse("1233333333333333"), 
                BigInteger.Parse("9400000000000000000000000000"),
                BigInteger.Parse("1223123"), 
                BigInteger.Parse("544343434343434343"),
                BigInteger.Parse("544343434343434343"),
                BigInteger.Parse("544343434343434343"),
                BigInteger.Parse("544343434343434343"),
                BigInteger.Parse("544343434343434343"),
                BigInteger.Parse("544343434343434343"),
                BigInteger.Parse("544343434343434343"),
                BigInteger.Parse("544343434343434343"),
                BigInteger.Parse("544343434343434343"),
                BigInteger.Parse("544343434343434343"),
            };
            BigInteger testThreePoseidonHash = poseidonThree.CalculatePoseidonHash(inputsThree);
            Debug.Assert(testThreePoseidonHash == BigInteger.Parse("5672127111078700825511016759205848053541633732578151671665123112660080656153"), "Hash doesn't match expected hash!");
            Console.WriteLine($"Hash of test three is {testThreePoseidonHash}");
            Eddsa eddsaThree = new Eddsa(testThreePoseidonHash, Environment.GetEnvironmentVariable("LoopringPrivateKey", EnvironmentVariableTarget.User)); //Put in the calculated poseidon hash in order to Sign
            string signedMessageThree = eddsaThree.Sign();
            Debug.Assert(signedMessageThree == "0x152b971f5796226639add0a1572e348605a4291675fd10f7b8fa989057246e9815fc8e98e246ce212355d49fc4df5a0a6024ee3604164f3c2227b4380e9150c70427e5913a51bc85ead489ef28c97fe85aa399da0d37cb7ff845d511e44a2d50", "Signed message doesn't match expected signed message");
            Console.WriteLine($"Signed message: {signedMessageThree}");

            Console.WriteLine("Enter to exit");
            Console.ReadKey();
        }
    }
}
