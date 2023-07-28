using System;

namespace HospitalLibrary.Exceptions;

public class UcidExistsException : Exception
{
    public UcidExistsException(string message) : base(message)
    {
    }
}