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
  [BepInPlugin("000.MultiSelection", "Multiple Characters Menu Select Mod", "2.0.0")]
  public class Mainspace : BaseUnityPlugin
  {
    public void Awake() => Add();

    public static void Add()
    {
      IDetour idetour1 = new Hook(typeof(CharacterDataBase).GetMethod(nameof(CharacterDataBase.PrepareCharacters), ~BindingFlags.Default), typeof(Hooks).GetMethod(nameof(Hooks.PrepareCharacters), ~BindingFlags.Default));
      IDetour idetour2 = new Hook(typeof(CharacterSelectionHandler).GetMethod(nameof(CharacterSelectionHandler.OnCompanionCharacterSelected), ~BindingFlags.Default), typeof(Hooks).GetMethod(nameof(Hooks.OnCompanionCharacterSelected), ~BindingFlags.Default));
      IDetour idetour3 = new Hook(typeof(CharacterSelectionHandler).GetMethod(nameof(CharacterSelectionHandler.OnThirdCharacterSelected), ~BindingFlags.Default), typeof(Hooks).GetMethod(nameof(Hooks.OnThirdCharacterSelected), ~BindingFlags.Default));
      IDetour idetour4 = new Hook(typeof(CharacterSelectionHandler).GetMethod(nameof(CharacterSelectionHandler.OnMainCharacterSelected), ~BindingFlags.Default), typeof(Hooks).GetMethod(nameof(Hooks.OnMainCharacterSelected), ~BindingFlags.Default));
      IDetour idetour5 = new Hook(typeof (CharacterSelectionHandler).GetMethod(nameof(CharacterSelectionHandler.OnRandomCharacterSelected), ~BindingFlags.Default), typeof (Hooks).GetMethod(nameof(Hooks.OnRandomCharacterSelected), ~BindingFlags.Default));
      IDetour idetour6 = new Hook(typeof(CharacterSelectionHandler).GetMethod(nameof(CharacterSelectionHandler.OnCharacterSelected), ~BindingFlags.Default), typeof(Hooks).GetMethod(nameof(Hooks.OnCharacterSelected), ~BindingFlags.Default));
      IDetour idetour7 = new Hook(typeof(CharacterSelectionHandler).GetMethod(nameof(CharacterSelectionHandler.Activation), ~BindingFlags.Default), typeof(Hooks).GetMethod(nameof(Hooks.Activation), ~BindingFlags.Default));
      IDetour idetour8 = new Hook(typeof (SelectableCharacterInformationLayout).GetMethod(nameof(SelectableCharacterInformationLayout.OnOptionSelected), ~BindingFlags.Default), typeof (Hooks).GetMethod(nameof(Hooks.OnOptionSelected), ~BindingFlags.Default));
      IDetour idetour9 = new Hook(typeof (MainMenuController).GetMethod(nameof(MainMenuController.Start), ~BindingFlags.Default), typeof (Hooks).GetMethod(nameof(Hooks.Start), ~BindingFlags.Default));
      IDetour idetourA = new Hook(typeof (MainMenuController).GetMethod(nameof(MainMenuController.PrepareNewRunData), ~BindingFlags.Default), typeof (Hooks).GetMethod(nameof(Hooks.PrepareNewRunData), ~BindingFlags.Default));
    }
  }
}
