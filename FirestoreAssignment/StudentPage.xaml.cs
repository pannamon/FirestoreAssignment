using FirestoreAssignment.Services;
using FirestoreAssignment.ViewStudent;

namespace FirestoreAssignment;

public partial class StudentPage : ContentPage
{
	public StudentPage()
	{
		InitializeComponent();
		var firestoreService = new StudentService();
		BindingContext = new SampleViewStudent(firestoreService);
	}
}