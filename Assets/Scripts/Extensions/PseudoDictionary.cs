using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    [System.Serializable]
    public class PseudoDictionary<T, TU>
    {
    // PSEUDO DICTIONARY ENTRIES
    // & DICTIONARY CONVERSION
 
    [SerializeField] private List<PseudoKeyValuePair<T, TU>> entries;
    private Dictionary<T, TU> _actualDictionary = new();
 
    // COUNT
 
    public int Count
    {
        get
        {
            _actualDictionary = FromPseudoDictionaryToActualDictionary();
            return _actualDictionary.Count;
        }
    }
 
    // INDEXER
 
    public TU this[T index]
    {
        get
        {
            _actualDictionary = FromPseudoDictionaryToActualDictionary();
            return _actualDictionary[index];
        }
    }
 
    // FROM DICTIONARY TO PSEUDO
 
    public List<PseudoKeyValuePair<T, TU>> FromActualDictionaryToPseudoDictionary(Dictionary<T, TU> actualDictionary)
    {
        List<PseudoKeyValuePair<T, TU>> pseudoDictionary = new();
 
        foreach (KeyValuePair<T, TU> pair in actualDictionary)
            pseudoDictionary.Add(new(pair.Key, pair.Value));
 
        return pseudoDictionary;
    }
 
    public List<PseudoKeyValuePair<T, TU>> FromActualDictionaryToPseudoDictionary()
        => FromActualDictionaryToPseudoDictionary(_actualDictionary);
 
    // FROM PSEUDO TO DICTIONARY
 
    public Dictionary<T, TU> FromPseudoDictionaryToActualDictionary(List<PseudoKeyValuePair<T, TU>> pseudoDictionary)
    {
        Dictionary<T, TU> dictionary = new();
 
        foreach (PseudoKeyValuePair<T, TU> entry in pseudoDictionary)
            dictionary.Add(entry.Key, entry.Value);
 
        return dictionary;
    }
 
    public Dictionary<T, TU> FromPseudoDictionaryToActualDictionary()
        => FromPseudoDictionaryToActualDictionary(entries);
 
    // OPERATIONS
 
    public void Add(T key, TU value)
    {
        _actualDictionary = FromPseudoDictionaryToActualDictionary();
        _actualDictionary.Add(key, value);
        entries = FromActualDictionaryToPseudoDictionary();
    }
 
    public void Remove(T key)
    {
        _actualDictionary = FromPseudoDictionaryToActualDictionary();
        _actualDictionary.Remove(key);
        entries = FromActualDictionaryToPseudoDictionary();
    }
 
    public void Clear()
    {
        _actualDictionary.Clear();
        entries = new();
    }
 
    public TU TryGetValue(T key)
    {
        _actualDictionary = FromPseudoDictionaryToActualDictionary();
        _actualDictionary.TryGetValue(key, out var value);
        return value;
    }
}

    [System.Serializable]
    public struct PseudoKeyValuePair<T, TU>
    {
        [SerializeField] private T key;
        [SerializeField] private TU value;

        public T Key => key;

        public TU Value => value;

        public PseudoKeyValuePair(T key, TU value)
        {
            this.key = key;
            this.value = value;
        }
    }
}