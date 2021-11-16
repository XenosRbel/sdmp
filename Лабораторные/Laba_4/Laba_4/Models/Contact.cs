using System.Collections.Generic;

namespace Laba_4.Models
{
	public class Contact : IBaseEntity
	{
		public string PhotoPath { get; set; }
		public string Name { get; set; }
		public string PhoneNumber { get; set; }
		public string Region { get; set; }
		public string ImageUrl { get; set; }
		public ContactType PhoneType { get; set; }
		public int Id { get; set; }

		public override bool Equals(object obj)
		{
			return obj is Contact contact &&
				   PhotoPath == contact.PhotoPath &&
				   Name == contact.Name &&
				   PhoneNumber == contact.PhoneNumber &&
				   PhoneType == contact.PhoneType &&
				   Id == contact.Id;
		}

		public override int GetHashCode()
		{
			var hashCode = 1018803031;
			hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PhotoPath);
			hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
			hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PhoneNumber);
			hashCode = hashCode * -1521134295 + PhoneType.GetHashCode();
			hashCode = hashCode * -1521134295 + Id.GetHashCode();
			return hashCode;
		}

		public static bool operator ==(Contact left, Contact right)
		{
			return EqualityComparer<Contact>.Default.Equals(left, right);
		}

		public static bool operator !=(Contact left, Contact right)
		{
			return !(left == right);
		}
	}

	public class ContactIdComparer : IEqualityComparer<Contact>
	{
		public bool Equals(Contact x, Contact y)
		{
			return x.Id == y.Id;
		}

		public int GetHashCode(Contact obj)
		{
			return obj.GetHashCode();
		}
	}

	public enum ContactType
	{
		None,
		WorkPhone
	}
}
