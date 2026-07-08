namespace PrenumerantAPI.Models
{
	public class Prenumerant
	{
		public int PrenumerationsNummer {get; set;}
		public string PersonNummer {get; set;} = string.Empty;
		public string ForNamn {get; set;} = string.Empty; 
		public string EfterNamn {get; set;} = string.Empty;
		public string UtdelningsAdress {get; set;} = string.Empty;
		public string PostNummer {get; set;} = string.Empty;
		public string Ort {get; set;} = string.Empty;
		public string TelefonNummer {get; set;} = string.Empty;
	}
}