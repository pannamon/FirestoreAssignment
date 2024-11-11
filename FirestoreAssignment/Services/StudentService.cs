using System;
using FirestoreAssignment.Models;
using Google.Cloud.Firestore;

namespace FirestoreAssignment.Services;

public class StudentService

{
    private FirestoreDb db;
    public string StatusMessage;

    public StudentService()
    {
        this.SetupStudent();
    }

       private async Task SetupStudent()
    {
        if (db == null)
        {
            var stream = await FileSystem.OpenAppPackageFileAsync("fir-assignment-67525-firebase-adminsdk-1c19l-eeb21da05a.json");
            var reader = new StreamReader(stream);
            var contents = reader.ReadToEnd();
            db = new FirestoreDbBuilder
            {
                ProjectId = "fir-assignment-67525",

                JsonCredentials = contents
            }.Build();
        }
    }

     public async Task<List<SampleStudent>> GetAllSample()
    {
        try
        {
            await SetupStudent();
            var data = await db.Collection("Students").GetSnapshotAsync();
            var students = data.Documents.Select(doc =>
            {
                var student = new SampleStudent();
                student.id = doc.GetValue<string>("id");
                student.Code = doc.GetValue<string>("Code");
                student.Name = doc.GetValue<string>("Name");

                return student;
            }).ToList();
            return students;
        }
        catch (Exception ex)
        {

            StatusMessage = $"Error: {ex.Message}";
        }
        return null;
    }

 public async Task InsertStudent(SampleStudent student)
    {
        try
        {
            await SetupStudent();
            var studentData = new Dictionary<string, object>
            {
                { "id", student.id },
                { "Code", student.Code },
                { "Name", student.Name }
               
            };

            await db.Collection("Students").AddAsync(studentData);
        }
        catch (Exception ex)
        {

            StatusMessage = $"Error: {ex.Message}";
        }
    }

      public async Task UpdateStudent(SampleStudent student)
    {
        try
        {
            await SetupStudent();

            
            var studentData = new Dictionary<string, object>
            {
                { "id", student.id },
                { "Code", student.Code },
                { "Name", student.Name }
               
            };

            
            var docRef = db.Collection("Students").Document(student.id);
            await docRef.SetAsync(studentData, SetOptions.Overwrite);

            StatusMessage = "Student successfully updated!";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error: {ex.Message}";
        }
    }

 public async Task DeleteStudent(string id)
    {
        try
        {
            await SetupStudent();

           
            var docRef = db.Collection("Students").Document(id);
            await docRef.DeleteAsync();

            StatusMessage = "Student successfully deleted!";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error: {ex.Message}";
        }
    }






}
