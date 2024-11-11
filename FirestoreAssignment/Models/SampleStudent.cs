using System;
using Google.Cloud.Firestore;

namespace FirestoreAssignment.Models;

public class SampleStudent
{
    [FirestoreProperty]
   public string id {get; set;}

    [FirestoreProperty]
    public string Code {get; set;}

     [FirestoreProperty]
     public string Name {get; set;}

}
