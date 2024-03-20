// Decompiled with JetBrains decompiler
// Type: MultiSelectMod.Mainspace
// Assembly: MultiSelectMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2CD6A45B-178B-4FBB-9F40-924F21DA07D1
// Assembly location: C:\Users\windows\Downloads\MultiSelectMod.dll

using BepInEx;
using MonoMod.RuntimeDetour;
using System.Reflection;

#nullable disable
namespace MultiSelectMod
{
  [BepInPlugin("000.MultiSelection", "Multiple Characters Menu Select Mod", "1.0.7")]
  public class Mainspace : BaseUnityPlugin
  {
    public void Awake() => Add();

    public static void Add()
    {
      IDetour idetour1 = new Hook(typeof(SelectableCharactersSO).GetMethod("PrepareCharacters", ~BindingFlags.Default), typeof(Hooks).GetMethod("PrepareCharacters", ~BindingFlags.Default));
      IDetour idetour2 = new Hook(typeof(CharacterSelectionHandler).GetMethod("OnCompanionCharacterSelected", ~BindingFlags.Default), typeof(Hooks).GetMethod("OnCompanionCharacterSelected", ~BindingFlags.Default));
      IDetour idetour3 = new Hook(typeof(CharacterSelectionHandler).GetMethod("OnThirdCharacterSelected", ~BindingFlags.Default), typeof(Hooks).GetMethod("OnThirdCharacterSelected", ~BindingFlags.Default));
      IDetour idetour4 = new Hook(typeof(CharacterSelectionHandler).GetMethod("OnMainCharacterSelected", ~BindingFlags.Default), typeof(Hooks).GetMethod("OnMainCharacterSelected", ~BindingFlags.Default));
      IDetour idetour5 = new Hook(typeof (CharacterSelectionHandler).GetMethod("OnRandomCharacterSelected", ~BindingFlags.Default), typeof (Hooks).GetMethod("OnRandomCharacterSelected", ~BindingFlags.Default));
      IDetour idetour6 = new Hook(typeof(CharacterSelectionHandler).GetMethod("OnCharacterSelected", ~BindingFlags.Default), typeof(Hooks).GetMethod("OnCharacterSelected", ~BindingFlags.Default));
      IDetour idetour7 = new Hook(typeof(CharacterSelectionHandler).GetMethod("Activation", ~BindingFlags.Default), typeof(Hooks).GetMethod("Activation", ~BindingFlags.Default));
      IDetour idetour8 = new Hook(typeof (SelectableCharacterInformationLayout).GetMethod("OnOptionSelected", ~BindingFlags.Default), typeof (Hooks).GetMethod("OnOptionSelected", ~BindingFlags.Default));
      IDetour idetour9 = new Hook(typeof (MainMenuController).GetMethod("Start", ~BindingFlags.Default), typeof (Hooks).GetMethod("Start", ~BindingFlags.Default));
      IDetour idetourA = new Hook(typeof (MainMenuController).GetMethod("PrepareNewRunData", ~BindingFlags.Default), typeof (Hooks).GetMethod("PrepareNewRunData", ~BindingFlags.Default));
    }
  }
}
