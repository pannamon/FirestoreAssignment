using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using FirestoreAssignment.Models;
using FirestoreAssignment.Services;
using PropertyChanged;

namespace FirestoreAssignment.ViewStudent;

[AddINotifyPropertyChangedInterface]
public class SampleViewStudent

{
    StudentService _studentService;

    public ObservableCollection<SampleStudent> Samples { get; set; } = [];
    public SampleStudent CurrentSample { get; set; }

    public ICommand Reset { get; set; }
    public ICommand AddOrUpdateCommand { get; set; }
    public ICommand DeleteCommand { get; set; }

  public SampleViewStudent (StudentService studentService)
    {
        this._studentService = studentService;
        this.Refresh();
        Reset = new Command( async () =>
        {
            CurrentSample = new SampleStudent();
            await this.Refresh();
        }
        );
        AddOrUpdateCommand = new Command(async () =>
        {
            await this.Save();
            await this.Refresh();
        });
        DeleteCommand = new Command(async () =>
        {
            await this.Delete();
            await this.Refresh();
        });
    }

  public async Task GetAll()
    {
        Samples = [];
        var items = await _studentService.GetAllSample();
        foreach (var item in items)
        {
            Samples.Add(item);
        }
    }

    public async Task Save()
    {
       if(string.IsNullOrEmpty(CurrentSample.id))
       {
            await _studentService.InsertStudent(this.CurrentSample);
       }
       else{
            await _studentService.UpdateStudent(this.CurrentSample);
       }
    }

    private async Task Refresh()
    {
        CurrentSample = new SampleStudent();
        await this.GetAll();
    }

    private async Task Delete()
    {
        await _studentService.DeleteStudent(this.CurrentSample.id);
    }


}
