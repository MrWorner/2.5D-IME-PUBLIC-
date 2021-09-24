using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MG_Property
{
    public string ID { get; set; }//айди свойства
    public string Name { get; set; } //имя свойства
    public Sprite Icon { get; set; } //NEW иконка свойства !ЗАДАЕТСЯ В БИБЛИОТЕКЕ!
}
