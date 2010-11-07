﻿#region License
// Copyright (c) 2007 James Newton-King
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
#endregion

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
#if !(NET35 || NET20 || SILVERLIGHT)
using System.Dynamic;
using System.Linq.Expressions;
#endif
using System.Linq;
using System.IO;
using Newtonsoft.Json.Utilities;
using System.Globalization;
#if !PocketPC && !SILVERLIGHT
using Newtonsoft.Json.Linq.ComponentModel;
#endif

namespace Newtonsoft.Json.Linq
{
  /// <summary>
  /// Represents a JSON object.
  /// </summary>
  public class JObject : JContainer, IDictionary<string, JToken>, INotifyPropertyChanged
#if !(PocketPC || SILVERLIGHT)
    , ICustomTypeDescriptor
#endif
#if !(PocketPC || SILVERLIGHT || NET20)
    , INotifyPropertyChanging
#endif
  {
    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

#if !(PocketPC || SILVERLIGHT || NET20)
    /// <summary>
    /// Occurs when a property value is changing.
    /// </summary>
    public event PropertyChangingEventHandler PropertyChanging;
#endif

    /// <summary>
    /// Initializes a new instance of the <see cref="JObject"/> class.
    /// </summary>
    public JObject()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="JObject"/> class from another <see cref="JObject"/> object.
    /// </summary>
    /// <param name="other">A <see cref="JObject"/> object to copy from.</param>
    public JObject(JObject other)
      : base(other)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="JObject"/> class with the specified content.
    /// </summary>
    /// <param name="content">The contents of the object.</param>
    public JObject(params object[] content)
      : this((object)content)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="JObject"/> class with the specified content.
    /// </summary>
    /// <param name="content">The contents of the object.</param>
    public JObject(object content)
    {
      Add(content);
    }

    internal override bool DeepEquals(JToken node)
    {
      JObject t = node as JObject;
      return (t != null && ContentsEqual(t));
    }

    internal override void ValidateToken(JToken o, JToken existing)
    {
      ValidationUtils.ArgumentNotNull(o, "o");

      if (o.Type != JTokenType.Property)
        throw new ArgumentException("Can not add {0} to {1}.".FormatWith(CultureInfo.InvariantCulture, o.GetType(), GetType()));

      // looping over all properties every time isn't good
      // need to think about performance here
      JProperty property = (JProperty)o;
      foreach (JProperty childProperty in Children())
      {
        if (childProperty != existing && string.Equals(childProperty.Name, property.Name, StringComparison.Ordinal))
          throw new ArgumentException("Can not add property {0} to {1}. Property with the same name already exists on object.".FormatWith(CultureInfo.InvariantCulture, property.Name, GetType()));
      }
    }

    internal void InternalPropertyChanged(JProperty childProperty)
    {
      OnPropertyChanged(childProperty.Name);
#if !SILVERLIGHT
      OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, IndexOfItem(childProperty)));
#endif
#if SILVERLIGHT || !(NET20 || NET35)
      OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, childProperty, childProperty, IndexOfItem(childProperty)));
#endif
    }

    internal void InternalPropertyChanging(JProperty childProperty)
    {
#if !PocketPC && !SILVERLIGHT && !NET20
      OnPropertyChanging(childProperty.Name);
#endif
    }

    internal override JToken CloneToken()
    {
      return new JObject(this);
    }

    /// <summary>
    /// Gets the node type for this <see cref="JToken"/>.
    /// </summary>
    /// <value>The type.</value>
    public override JTokenType Type
    {
      get { return JTokenType.Object; }
    }

    /// <summary>
    /// Gets an <see cref="IEnumerable{JProperty}"/> of this object's properties.
    /// </summary>
    /// <returns>An <see cref="IEnumerable{JProperty}"/> of this object's properties.</returns>
    public IEnumerable<JProperty> Properties()
    {
      return Children().Cast<JProperty>();
    }

    /// <summary>
    /// Gets a <see cref="JProperty"/> the specified name.
    /// </summary>
    /// <param name="name">The property name.</param>
    /// <returns>A <see cref="JProperty"/> with the specified name or null.</returns>
    public JProperty Property(string name)
    {
      return Properties()
        .Where(p => string.Equals(p.Name, name, StringComparison.Ordinal))
        .SingleOrDefault();
    }

    /// <summary>
    /// Gets an <see cref="JEnumerable{JToken}"/> of this object's property values.
    /// </summary>
    /// <returns>An <see cref="JEnumerable{JToken}"/> of this object's property values.</returns>
    public JEnumerable<JToken> PropertyValues()
    {
      return new JEnumerable<JToken>(Properties().Select(p => p.Value));
    }

    /// <summary>
    /// Gets the <see cref="JToken"/> with the specified key.
    /// </summary>
    /// <value>The <see cref="JToken"/> with the specified key.</value>
    public override JToken this[object key]
    {
      get
      {
        ValidationUtils.ArgumentNotNull(key, "o");

        string propertyName = key as string;
        if (propertyName == null)
          throw new ArgumentException("Accessed JObject values with invalid key value: {0}. Object property name expected.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));

        return this[propertyName];
      }
      set
      {
        ValidationUtils.ArgumentNotNull(key, "o");

        string propertyName = key as string;
        if (propertyName == null)
          throw new ArgumentException("Set JObject values with invalid key value: {0}. Object property name expected.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));

        this[propertyName] = value;
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="Newtonsoft.Json.Linq.JToken"/> with the specified property name.
    /// </summary>
    /// <value></value>
    public JToken this[string propertyName]
    {
      get
      {
        ValidationUtils.ArgumentNotNull(propertyName, "propertyName");

        JProperty property = Property(propertyName);

        return (property != null) ? property.Value : null;
      }
      set
      {
        JProperty property = Property(propertyName);
        if (property != null)
        {
          property.Value = value;
        }
        else
        {
#if !PocketPC && !SILVERLIGHT && !NET20
          OnPropertyChanging(propertyName);
#endif
          Add(new JProperty(propertyName, value));
          OnPropertyChanged(propertyName);
        }
      }
    }

    /// <summary>
    /// Loads an <see cref="JObject"/> from a <see cref="JsonReader"/>. 
    /// </summary>
    /// <param name="reader">A <see cref="JsonReader"/> that will be read for the content of the <see cref="JObject"/>.</param>
    /// <returns>A <see cref="JObject"/> that contains the JSON that was read from the specified <see cref="JsonReader"/>.</returns>
    public static JObject Load(JsonReader reader)
    {
      ValidationUtils.ArgumentNotNull(reader, "reader");

      if (reader.TokenType == JsonToken.None)
      {
        if (!reader.Read())
          throw new Exception("Error reading JObject from JsonReader.");
      }
      if (reader.TokenType != JsonToken.StartObject)
        throw new Exception(
          "Error reading JObject from JsonReader. Current JsonReader item is not an object: {0}".FormatWith(
            CultureInfo.InvariantCulture, reader.TokenType));

      JObject o = new JObject();
      o.SetLineInfo(reader as IJsonLineInfo);
      
      if (!reader.Read())
        throw new Exception("Error reading JObject from JsonReader.");

      o.ReadContentFrom(reader);

      return o;
    }

    /// <summary>
    /// Load a <see cref="JObject"/> from a string that contains JSON.
    /// </summary>
    /// <param name="json">A <see cref="String"/> that contains JSON.</param>
    /// <returns>A <see cref="JObject"/> populated from the string that contains JSON.</returns>
    public static JObject Parse(string json)
    {
      JsonReader jsonReader = new JsonTextReader(new StringReader(json));

      return Load(jsonReader);
    }

    /// <summary>
    /// Creates a <see cref="JObject"/> from an object.
    /// </summary>
    /// <param name="o">The object that will be used to create <see cref="JObject"/>.</param>
    /// <returns>A <see cref="JObject"/> with the values of the specified object</returns>
    public static new JObject FromObject(object o)
    {
      return FromObject(o, new JsonSerializer());
    }

    /// <summary>
    /// Creates a <see cref="JArray"/> from an object.
    /// </summary>
    /// <param name="o">The object that will be used to create <see cref="JArray"/>.</param>
    /// <param name="jsonSerializer">The <see cref="JsonSerializer"/> that will be used to read the object.</param>
    /// <returns>A <see cref="JArray"/> with the values of the specified object</returns>
    public static new JObject FromObject(object o, JsonSerializer jsonSerializer)
    {
      JToken token = FromObjectInternal(o, jsonSerializer);

      if (token != null && token.Type != JTokenType.Object)
        throw new ArgumentException("Object serialized to {0}. JObject instance expected.".FormatWith(CultureInfo.InvariantCulture, token.Type));

      return (JObject)token;
    }

    /// <summary>
    /// Writes this token to a <see cref="JsonWriter"/>.
    /// </summary>
    /// <param name="writer">A <see cref="JsonWriter"/> into which this method will write.</param>
    /// <param name="converters">A collection of <see cref="JsonConverter"/> which will be used when writing the token.</param>
    public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
    {
      writer.WriteStartObject();

      foreach (JProperty property in ChildrenInternal())
      {
        property.WriteTo(writer, converters);
      }

      writer.WriteEndObject();
    }

    #region IDictionary<string,JToken> Members
    /// <summary>
    /// Adds the specified property name.
    /// </summary>
    /// <param name="propertyName">Name of the property.</param>
    /// <param name="value">The value.</param>
    public void Add(string propertyName, JToken value)
    {
      Add(new JProperty(propertyName, value));
    }

    bool IDictionary<string, JToken>.ContainsKey(string key)
    {
      return (Property(key) != null);
    }

    ICollection<string> IDictionary<string, JToken>.Keys
    {
      get { throw new NotImplementedException(); }
    }

    /// <summary>
    /// Removes the property with the specified name.
    /// </summary>
    /// <param name="propertyName">Name of the property.</param>
    /// <returns>true if item was successfully removed; otherwise, false.</returns>
    public bool Remove(string propertyName)
    {
      JProperty property = Property(propertyName);
      if (property == null)
        return false;

      property.Remove();
      return true;
    }

    /// <summary>
    /// Tries the get value.
    /// </summary>
    /// <param name="propertyName">Name of the property.</param>
    /// <param name="value">The value.</param>
    /// <returns>true if a value was successfully retrieved; otherwise, false.</returns>
    public bool TryGetValue(string propertyName, out JToken value)
    {
      JProperty property = Property(propertyName);
      if (property == null)
      {
        value = null;
        return false;
      }

      value = property.Value;
      return true;
    }

    ICollection<JToken> IDictionary<string, JToken>.Values
    {
      get { throw new NotImplementedException(); }
    }

    #endregion

    #region ICollection<KeyValuePair<string,JToken>> Members

    void ICollection<KeyValuePair<string,JToken>>.Add(KeyValuePair<string, JToken> item)
    {
      Add(new JProperty(item.Key, item.Value));
    }

    void ICollection<KeyValuePair<string, JToken>>.Clear()
    {
      RemoveAll();
    }

    bool ICollection<KeyValuePair<string,JToken>>.Contains(KeyValuePair<string, JToken> item)
    {
      JProperty property = Property(item.Key);
      if (property == null)
        return false;

      return (property.Value == item.Value);
    }

    void ICollection<KeyValuePair<string,JToken>>.CopyTo(KeyValuePair<string, JToken>[] array, int arrayIndex)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      if (arrayIndex < 0)
        throw new ArgumentOutOfRangeException("arrayIndex", "arrayIndex is less than 0.");
      if (arrayIndex >= array.Length)
        throw new ArgumentException("arrayIndex is equal to or greater than the length of array.");
      if (Count > array.Length - arrayIndex)
        throw new ArgumentException("The number of elements in the source JObject is greater than the available space from arrayIndex to the end of the destination array.");

      int index = 0;
      foreach (JProperty property in Properties())
      {
        array[arrayIndex + index] = new KeyValuePair<string, JToken>(property.Name, property.Value);
        index++;
      }
    }

    /// <summary>
    /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
    /// </summary>
    /// <value></value>
    /// <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.</returns>
    public int Count
    {
      get { return Children().Count(); }
    }

    bool ICollection<KeyValuePair<string,JToken>>.IsReadOnly
    {
      get { return false; }
    }

    bool ICollection<KeyValuePair<string,JToken>>.Remove(KeyValuePair<string, JToken> item)
    {
      if (!((ICollection<KeyValuePair<string,JToken>>)this).Contains(item))
        return false;

      ((IDictionary<string, JToken>)this).Remove(item.Key);
      return true;
    }

    #endregion

    internal override int GetDeepHashCode()
    {
      return ContentsHashCode();
    }

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
    /// </returns>
    public IEnumerator<KeyValuePair<string, JToken>> GetEnumerator()
    {
      foreach (JProperty property in Properties())
      {
        yield return new KeyValuePair<string, JToken>(property.Name, property.Value);
      }
    }

    /// <summary>
    /// Raises the <see cref="PropertyChanged"/> event with the provided arguments.
    /// </summary>
    /// <param name="propertyName">Name of the property.</param>
    protected virtual void OnPropertyChanged(string propertyName)
    {
      if (PropertyChanged != null)
        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }

#if !PocketPC && !SILVERLIGHT && !NET20
    /// <summary>
    /// Raises the <see cref="PropertyChanging"/> event with the provided arguments.
    /// </summary>
    /// <param name="propertyName">Name of the property.</param>
    protected virtual void OnPropertyChanging(string propertyName)
    {
      if (PropertyChanging != null)
        PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
    }
#endif

#if !PocketPC && !SILVERLIGHT
    // include custom type descriptor on JObject rather than use a provider because the properties are specific to a type
    #region ICustomTypeDescriptor
    /// <summary>
    /// Returns the properties for this instance of a component.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.ComponentModel.PropertyDescriptorCollection"/> that represents the properties for this component instance.
    /// </returns>
    PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
    {
      return ((ICustomTypeDescriptor) this).GetProperties(null);
    }

    private static Type GetTokenPropertyType(JToken token)
    {
      if (token is JValue)
      {
        JValue v = (JValue)token;
        return (v.Value != null) ? v.Value.GetType() : typeof(object);
      }

      return token.GetType();
    }

    /// <summary>
    /// Returns the properties for this instance of a component using the attribute array as a filter.
    /// </summary>
    /// <param name="attributes">An array of type <see cref="T:System.Attribute"/> that is used as a filter.</param>
    /// <returns>
    /// A <see cref="T:System.ComponentModel.PropertyDescriptorCollection"/> that represents the filtered properties for this component instance.
    /// </returns>
    PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
    {
      PropertyDescriptorCollection descriptors = new PropertyDescriptorCollection(null);

      foreach (KeyValuePair<string, JToken> propertyValue in this)
      {
        descriptors.Add(new JPropertyDescriptor(propertyValue.Key, GetTokenPropertyType(propertyValue.Value)));
      }

      return descriptors;
    }

    /// <summary>
    /// Returns a collection of custom attributes for this instance of a component.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.ComponentModel.AttributeCollection"/> containing the attributes for this object.
    /// </returns>
    AttributeCollection ICustomTypeDescriptor.GetAttributes()
    {
      return AttributeCollection.Empty;
    }

    /// <summary>
    /// Returns the class name of this instance of a component.
    /// </summary>
    /// <returns>
    /// The class name of the object, or null if the class does not have a name.
    /// </returns>
    string ICustomTypeDescriptor.GetClassName()
    {
      return null;
    }

    /// <summary>
    /// Returns the name of this instance of a component.
    /// </summary>
    /// <returns>
    /// The name of the object, or null if the object does not have a name.
    /// </returns>
    string ICustomTypeDescriptor.GetComponentName()
    {
      return null;
    }

    /// <summary>
    /// Returns a type converter for this instance of a component.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.ComponentModel.TypeConverter"/> that is the converter for this object, or null if there is no <see cref="T:System.ComponentModel.TypeConverter"/> for this object.
    /// </returns>
    TypeConverter ICustomTypeDescriptor.GetConverter()
    {
      return new TypeConverter();
    }

    /// <summary>
    /// Returns the default event for this instance of a component.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.ComponentModel.EventDescriptor"/> that represents the default event for this object, or null if this object does not have events.
    /// </returns>
    EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
    {
      return null;
    }

    /// <summary>
    /// Returns the default property for this instance of a component.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.ComponentModel.PropertyDescriptor"/> that represents the default property for this object, or null if this object does not have properties.
    /// </returns>
    PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
    {
      return null;
    }

    /// <summary>
    /// Returns an editor of the specified type for this instance of a component.
    /// </summary>
    /// <param name="editorBaseType">A <see cref="T:System.Type"/> that represents the editor for this object.</param>
    /// <returns>
    /// An <see cref="T:System.Object"/> of the specified type that is the editor for this object, or null if the editor cannot be found.
    /// </returns>
    object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
    {
      return null;
    }

    /// <summary>
    /// Returns the events for this instance of a component using the specified attribute array as a filter.
    /// </summary>
    /// <param name="attributes">An array of type <see cref="T:System.Attribute"/> that is used as a filter.</param>
    /// <returns>
    /// An <see cref="T:System.ComponentModel.EventDescriptorCollection"/> that represents the filtered events for this component instance.
    /// </returns>
    EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
    {
      return EventDescriptorCollection.Empty;
    }

    /// <summary>
    /// Returns the events for this instance of a component.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.ComponentModel.EventDescriptorCollection"/> that represents the events for this component instance.
    /// </returns>
    EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
    {
      return EventDescriptorCollection.Empty;
    }

    /// <summary>
    /// Returns an object that contains the property described by the specified property descriptor.
    /// </summary>
    /// <param name="pd">A <see cref="T:System.ComponentModel.PropertyDescriptor"/> that represents the property whose owner is to be found.</param>
    /// <returns>
    /// An <see cref="T:System.Object"/> that represents the owner of the specified property.
    /// </returns>
    object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
    {
      return null;
    }
    #endregion
#endif

#if !(NET35 || NET20 || SILVERLIGHT)
    /// <summary>
    /// Returns the <see cref="T:System.Dynamic.DynamicMetaObject"/> responsible for binding operations performed on this object.
    /// </summary>
    /// <param name="parameter">The expression tree representation of the runtime value.</param>
    /// <returns>
    /// The <see cref="T:System.Dynamic.DynamicMetaObject"/> to bind this object.
    /// </returns>
    protected override DynamicMetaObject GetMetaObject(Expression parameter)
    {
      return new DynamicProxyMetaObject<JObject>(parameter, new JObjectDynamicProxy(this), true);
    }

    private class JObjectDynamicProxy : DynamicProxy<JObject>
    {
      public JObjectDynamicProxy(JObject value) : base(value)
      {
      }

      public override bool TryGetMember(GetMemberBinder binder, out object result)
      {
        // result can be null
        result = Value[binder.Name];
        return true;
      }

      public override bool TrySetMember(SetMemberBinder binder, object value)
      {
        JToken v = value as JToken;

        // this can throw an error if value isn't a valid for a JValue
        if (v == null)
          v = new JValue(value);

        Value[binder.Name] = v;
        return true;
      }

      public override IEnumerable<string> GetDynamicMemberNames()
      {
        return Value.Properties().Select(p => p.Name);
      }
    }
#endif
  }
}