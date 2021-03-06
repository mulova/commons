﻿//----------------------------------------------
// Unity3D common libraries and editor tools
// License: The MIT License ( http://opensource.org/licenses/MIT )
// Copyright © 2013- mulova@gmail.com
//----------------------------------------------
using UnityEngine;
using mulova.commons;
using System.Text.Ex;

namespace mulova.unicore
{
	public static class TextureFormatEx
	{
		public static readonly TextureFormat[] ASTC = new TextureFormat[] {
			TextureFormat.ASTC_4x4,
			TextureFormat.ASTC_5x5,
			TextureFormat.ASTC_6x6,
			TextureFormat.ASTC_8x8,
			TextureFormat.ASTC_10x10,
			TextureFormat.ASTC_12x12,
			TextureFormat.ASTC_4x4,
			TextureFormat.ASTC_5x5,
			TextureFormat.ASTC_6x6,
			TextureFormat.ASTC_8x8,
			TextureFormat.ASTC_10x10,
			TextureFormat.ASTC_12x12,
		};
		
		public static readonly TextureFormat[] ETC2 = new TextureFormat[] {
			TextureFormat.ETC2_RGB,
			TextureFormat.ETC2_RGBA1,
			TextureFormat.ETC2_RGBA8,
			TextureFormat.EAC_R,
			TextureFormat.EAC_R_SIGNED,
			TextureFormat.EAC_RG,
			TextureFormat.EAC_RG_SIGNED,
		};
		
		public static readonly TextureFormat[] PVRTC = new TextureFormat[] {
			TextureFormat.PVRTC_RGB2,
			TextureFormat.PVRTC_RGB4,
			TextureFormat.PVRTC_RGBA2,
			TextureFormat.PVRTC_RGBA4,
		};
		
		public static readonly TextureFormat[] DXT = new TextureFormat[] {
			TextureFormat.DXT1,
			TextureFormat.DXT5,
		};
		
		public static readonly TextureFormat[] ETC = new TextureFormat[] {
			TextureFormat.ETC_RGB4,
		};
		
		
		public static bool IsCompressed(this TextureFormat format)
		{
			switch (format)
			{
				case TextureFormat.DXT1:
				case TextureFormat.DXT5:
				case TextureFormat.PVRTC_RGB2:
				case TextureFormat.PVRTC_RGBA2:
				case TextureFormat.PVRTC_RGB4:
				case TextureFormat.PVRTC_RGBA4:
				case TextureFormat.ETC_RGB4:
					return true;
			}
			return false;
		}
		
		public static bool IsASTC(this TextureFormat format)
		{
			return IsInGroup(format, ASTC);
		}
		
		public static bool IsETC2(this TextureFormat format)
		{
			return IsInGroup(format, ETC2);
		}
		
		public static bool IsPVRTC(this TextureFormat format)
		{
			return IsInGroup(format, PVRTC);
		}
		
		public static bool IsDXT(this TextureFormat format)
		{
			return IsInGroup(format, DXT);
		}
		
		public static bool IsETC(this TextureFormat format)
		{
			return IsInGroup(format, ETC);
		}
		
		private static bool IsInGroup(TextureFormat format, TextureFormat[] group)
		{
			foreach (TextureFormat f in group)
			{
				if (f == format)
				{
					return true;
				}
			}
			return false;
		}
		
		public static bool HasAlpha(this TextureFormat format)
		{
			switch (format)
			{
				case TextureFormat.Alpha8:
				case TextureFormat.ARGB4444:
				case TextureFormat.RGBA32:
				case TextureFormat.ARGB32:
				case TextureFormat.RGBA4444:
				case TextureFormat.PVRTC_RGBA2:
				case TextureFormat.PVRTC_RGBA4:
				case TextureFormat.BGRA32:
				case TextureFormat.RGBAHalf:
				case TextureFormat.DXT5:
				case TextureFormat.RGBAFloat:
				case TextureFormat.ETC2_RGBA1:
				case TextureFormat.ETC2_RGBA8:
				case TextureFormat.ASTC_4x4:
				case TextureFormat.ASTC_5x5:
				case TextureFormat.ASTC_6x6:
				case TextureFormat.ASTC_8x8:
				case TextureFormat.ASTC_10x10:
				case TextureFormat.ASTC_12x12:
					return true;
				case TextureFormat.RGB24:
				case TextureFormat.RGB565:
				case TextureFormat.DXT1:
				case TextureFormat.PVRTC_RGB2:
				case TextureFormat.PVRTC_RGB4:
				case TextureFormat.ETC_RGB4:
				case TextureFormat.R16:
				case TextureFormat.RHalf:
				case TextureFormat.RGHalf:
				case TextureFormat.RFloat:
				case TextureFormat.RGFloat:
				case TextureFormat.YUY2:
				case TextureFormat.EAC_R:
				case TextureFormat.EAC_R_SIGNED:
				case TextureFormat.EAC_RG:
				case TextureFormat.EAC_RG_SIGNED:
				case TextureFormat.ETC2_RGB:
					return false;
				default:
					return true;
			}
		}
		
		public static TextureFormat Get(string url)
		{
			if (url.EndsWithIgnoreCase(".png"))
			{
				return TextureFormat.RGBA32;
			} else if (url.EndsWithIgnoreCase(".jpg"))
			{
				return TextureFormat.RGB24;
			} else
			{
				return TextureFormat.DXT5;
			}
		}
	}
}

