using System;
using System.Net;

namespace HospitalLibrary.Exceptions;

public class EmailExistsException : Exception
{
    public EmailExistsException(string message) : base(message)
    {
        
    }
}