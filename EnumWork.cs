public static class EnumExtensions
{
	public static Dictionary<int, string> GetEnumList<T>() where T : struct, IConvertible // return Dictionary <value, Name> from all items of Enum type
  {
		if (!typeof(T).IsEnum)
			throw new Exception("enums only!");
		
        Array values = Enum.GetValues(typeof(T));
        Dictionary<int, string> items = new Dictionary<int, string>();

        foreach (var i in values)
        {	 
            items.Add( (int)i, ( Enum.Parse(typeof(T), i.ToString()) as Enum ).Name() );
        }
        return  items;
  }
	
   public static string Name(this Enum instance) //if Enum has an attribute Display Name= return it, else return name of item
   {
        string name = instance.ToString();
        MemberInfo field = instance.GetType().GetField(name);
        if (field != null)
          {
            System.ComponentModel.DataAnnotations.DisplayAttribute attribute = field.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.DisplayAttribute), false)
                  .Cast<System.ComponentModel.DataAnnotations.DisplayAttribute>().FirstOrDefault();
            if (attribute != null)
                name = attribute.Name;
          }
        return name;
   }
}
