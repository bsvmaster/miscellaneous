namespace EnumExtensions
public static class EnumExtensions
{
  // return Dictionary <value, Name> from all items of Enum type
  public static Dictionary<int, string> GetEnumDictionary<T>() where T : struct, IConvertible 
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
  
  // return List of SelectListItems from Enum
   public static List<System.Web.Mvc.SelectListItem> GetEnumSelectedList<T>() where T : struct, IConvertible //
   {
		if (!typeof(T).IsEnum)
			throw new Exception("enums only!");
		
        Array values = Enum.GetValues(typeof(T));
        var items = new List<System.Web.Mvc.SelectListItem>();

        foreach (var i in values)
        {	 
            items.Add(new System.Web.Mvc.SelectListItem{ Value = ((int)i).ToString(), Text = ( Enum.Parse(typeof(T), i.ToString()) as Enum ).Name()} );
        }
        return  items;
   }
	
   //if Enum has an attribute Display Name= return it, else return name of item
   public static string Name(this Enum instance) 
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

// -------- *** Using in ASP.NET MVC *** ---------
// in Controller:
// ViewBag.List = EnumExtensions.GetEnumSelectedList<MyEnumType>();

// in View:
// @Html.DropDownList("FieldName", (IEnumerable<SelectListItem>)ViewBag.List, "--- Chose item ---", null) 
