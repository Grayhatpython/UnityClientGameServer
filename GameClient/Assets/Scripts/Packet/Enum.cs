// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Enum.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Protocol {

  /// <summary>Holder for reflection information generated from Enum.proto</summary>
  public static partial class EnumReflection {

    #region Descriptor
    /// <summary>File descriptor for Enum.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static EnumReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CgpFbnVtLnByb3RvEghQcm90b2NvbCptCgpPYmplY3RUeXBlEhQKEE9CSkVD",
            "VF9UWVBFX05PTkUQABIYChRPQkpFQ1RfVFlQRV9DUkVBVFVSRRABEhMKD09C",
            "SkVDVF9UWVBFX0VOVhACEhoKFk9CSkVDVF9UWVBFX1BST0pFQ1RJTEUQAypy",
            "CgxDcmVhdHVyZVR5cGUSFgoSQ1JFQVRVUkVfVFlQRV9OT05FEAASGAoUQ1JF",
            "QVRVUkVfVFlQRV9QTEFZRVIQARIZChVDUkVBVFVSRV9UWVBFX01PTlNURVIQ",
            "AhIVChFDUkVBVFVSRV9UWVBFX05QQxADKl8KCU1vdmVTdGF0ZRITCg9NT1ZF",
            "X1NUQVRFX05PTkUQABITCg9NT1ZFX1NUQVRFX0lETEUQARISCg5NT1ZFX1NU",
            "QVRFX1JVThACEhQKEE1PVkVfU1RBVEVfU0tJTEwQAypoCgpQbGF5ZXJUeXBl",
            "EhQKEFBMQVlFUl9UWVBFX05PTkUQABIWChJQTEFZRVJfVFlQRV9LTklHSFQQ",
            "ARIUChBQTEFZRVJfVFlQRV9NQUdFEAISFgoSUExBWUVSX1RZUEVfQVJDSEVS",
            "EANiBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(new[] {typeof(global::Protocol.ObjectType), typeof(global::Protocol.CreatureType), typeof(global::Protocol.MoveState), typeof(global::Protocol.PlayerType), }, null, null));
    }
    #endregion

  }
  #region Enums
  public enum ObjectType {
    [pbr::OriginalName("OBJECT_TYPE_NONE")] None = 0,
    [pbr::OriginalName("OBJECT_TYPE_CREATURE")] Creature = 1,
    [pbr::OriginalName("OBJECT_TYPE_ENV")] Env = 2,
    [pbr::OriginalName("OBJECT_TYPE_PROJECTILE")] Projectile = 3,
  }

  public enum CreatureType {
    [pbr::OriginalName("CREATURE_TYPE_NONE")] None = 0,
    [pbr::OriginalName("CREATURE_TYPE_PLAYER")] Player = 1,
    [pbr::OriginalName("CREATURE_TYPE_MONSTER")] Monster = 2,
    [pbr::OriginalName("CREATURE_TYPE_NPC")] Npc = 3,
  }

  public enum MoveState {
    [pbr::OriginalName("MOVE_STATE_NONE")] None = 0,
    [pbr::OriginalName("MOVE_STATE_IDLE")] Idle = 1,
    [pbr::OriginalName("MOVE_STATE_RUN")] Run = 2,
    [pbr::OriginalName("MOVE_STATE_SKILL")] Skill = 3,
  }

  public enum PlayerType {
    [pbr::OriginalName("PLAYER_TYPE_NONE")] None = 0,
    [pbr::OriginalName("PLAYER_TYPE_KNIGHT")] Knight = 1,
    [pbr::OriginalName("PLAYER_TYPE_MAGE")] Mage = 2,
    [pbr::OriginalName("PLAYER_TYPE_ARCHER")] Archer = 3,
  }

  #endregion

}

#endregion Designer generated code
