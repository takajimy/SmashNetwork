﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmashNetworkPolymer.Models;

namespace SmashNetworkPolymer.Areas.Users.Models
{
    public class User : AbstractModel
    {
        public string Username { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }

        public virtual ICollection<Role> Roles { get; set; }

        public bool IsPasswordMatch(User userToCompare)
        {
            // Get password byte array
            byte[] passwordBytes = Encoding.UTF8.GetBytes(Hash);

            // Get salt byte array
            Salt salt = new Salt();
            salt.saltString = userToCompare.Salt;
            byte[] saltBytes = salt.getSaltBytes();

            // Compute hash byte array from password and salt
            Hash hash = new Hash();
            hash.computeHashBytes(passwordBytes, saltBytes);
            string hashString = hash.getHashString();

            // Compare the two
            if (userToCompare.Hash == hashString)
            {
                return true;
            }
            return false;
        }
    }

    public class Salt
    {
        private const int SALT_SIZE = 24;
        public byte[] saltBytes;
        public string saltString;
        private static RNGCryptoServiceProvider crypto;

        public Salt()
        {
            crypto = new RNGCryptoServiceProvider();
            saltString = "";
        }

        public void generateSaltBytes()
        {
            saltBytes = new byte[SALT_SIZE];
            crypto.GetNonZeroBytes(saltBytes);
        }

        public string getSaltString()
        {
            return Convert.ToBase64String(saltBytes);
        }

        public byte[] getSaltBytes()
        {
            return Convert.FromBase64String(saltString);
        }
    }

    public class Hash
    {
        private static SHA256CryptoServiceProvider sha;
        public byte[] hashBytes;
        public string hashString;

        public Hash()
        {
            sha = new SHA256CryptoServiceProvider();
        }

        public void computeHashBytes(byte[] passwordBytes, byte[] saltBytes)
        {
            byte[] passwordWithSaltBytes = new byte[passwordBytes.Length + saltBytes.Length];

            // Copy over password bytes
            for (int i = 0; i < passwordBytes.Length; i++)
            {
                passwordWithSaltBytes[i] = passwordBytes[i];
            }

            // Copy over salt bytes
            for (int i = 0; i < saltBytes.Length; i++)
            {
                passwordWithSaltBytes[passwordBytes.Length + i] = saltBytes[i];
            }

            // Generate the hash from the SHA algorithm
            hashBytes = sha.ComputeHash(passwordWithSaltBytes);
        }

        public string getHashString()
        {
            return Convert.ToBase64String(hashBytes);
        }

        public byte[] getHashBytes()
        {
            return Convert.FromBase64String(hashString);
        }
    }
}
