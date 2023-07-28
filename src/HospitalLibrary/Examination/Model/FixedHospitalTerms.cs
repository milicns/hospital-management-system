using System;

namespace HospitalLibrary.Examination.Model;

public struct FixedHospitalTerms
{
    public static readonly TimeSpan[] Terms = new[]
    {
        new TimeSpan(8, 0, 0),
        new TimeSpan(10,0,0),
        new TimeSpan(12,0,0),
        new TimeSpan(14,0,0),
        new TimeSpan(16,0,0),
        new TimeSpan(18,0,0),
    };
}