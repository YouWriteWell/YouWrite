
/* Below is description for all versions of RichEdit. Unfortunately, 
 * starting from version 4.1 there is no any documentation available. :-(
 * 
 * ### RichEdit 1.0 Features
 * Basic nonUnicode editing, cut/copy/paste, file streaming 
 * Basic set of character/paragraph formatting properties 
 * Message-based interface plus OLE interfaces: IRichEditOle and IRichEditOleCallback 
 * Vertical text and IME support (FE builds only). 
 * WYSIWYG editing using printer metrics 
 * Different builds for different scripts 
 * Common-control notifications plus some new ones 
 * Plain text and RTF files 
 * Pen-enabled and understood gestures for use with Pen Windows
 * 
 * 
 * ### RichEdit 2.0 Additions 
 * Unicode internally + able to read/write using codepages 
 * International line breaking algorithm 
 * Find Up/Down. Magellan mouse support. 
 * Multilevel undo 
 * BiDi (RE 2.1) and FE support including level 2/3 IME, dual font, keyboard linking, smart font apply 
 * AutoURL recognition. Word UI 
 * Plain/rich, single-line/multiline, scalable architecture 
 * Password and accelerator control options 
 * Windowless interfaces (ITextHost/ITextServices) 
 * Better display (mixed fonts use off-screen bitmap), system selection colors, transparency support 
 * TOM (Text Object Model) dual interfaces 
 * Character formatting additions include background color, locale ID, underline type, superscript/subscript. 
 * Paragraph formatting additions include space before/after, line spacing. 
 * Roundtrip all Word Format Font/Para dialog properties 
 * Extensive code stabilization, testing, performance increase
 * 
 * 
 * ### RichEdit 2.5 Additions 
 * First Windows CE version. Used by Pocket Word 
 * Outline view, normal and heading styles 
 * RTF additions 
 * Minor UI improvements 
 * Western languages only 
 * 
 * 
 * ### RichEdit 3.0 Additions 
 * Used for emulating RichEdit 1.0's 
 * Zoom 
 * Italics caret/cursor. URL hand cursor 
 * Paragraph numbering (alpha, numeric, Roman) 
 * Simple tables (no wrap in cells) 
 * More underline types, underline coloring, hidden text 
 * More of Word's default hot keys, e.g., accent dead keys, outline view, numbering 
 * Smart quotes (English only), soft hyphens 
 * Use Office's LineServices component to break/display lines. Used for complex scripts and options like center, right, decimal tabs, fully justified text 
 * Complex script support for BiDi, Indic, and Thai with help from LineServices and Uniscribe components 
 * Font Binding based on charset, which acts as writing system ID 
 * Codepage-specific stream in/out 
 * UTF-8 RTF. Used preferentially for cut/copy/paste. Can be streamed in/out. 
 * Office 9 IME support (MSIME98) including Reconversion, Document feed, Mouse Operation, and Caret position features 
 * AIMM component IME support for nonFE systems. 
 * Increased freeze and undo/redo control 
 * Font increment/decrement function 
 * System edit control, list box, and combo box controls 
 * Alt+x input method 
 * Used to emulate RichEdit 1.0's 
 * 
 * 
 * ### RichEdit 3.5 Additions 
 * Second Windows CE release. Used by eBooks 
 * Screen-size pagination 
 * Text wrap around objects flushed left/right 
 * Custom ClearType support 
 * Enhanced East Asian typography 
 * 
 * ### RichEdit 4.0 Additions 
 * Multilevel tables 
 * Autocorrect 
 * Improved autoURL detection 
 * Friendly name hyperlinks 
 * Font binding according to writing system (generalization of charset) 
 * Indic support 
 * Vertical text 
 * Support for the latest IMEs 
 * Speech and handwriting input (Windows Text Services Framework) 
 * More standard hot keys 
 * Many security fixes (3.0 has also) 
 * 
 * 
 * ### RichEdit 5.0 Additions 
 * Multiselection, smart drag&drop 
 * Better nested tables, horizontally merged cells 
 * Better font binding/international support 
 * More underline styles, small cap & shadow emulation 
 * Binary file format: "parsed XML" 
 * Partial XHTML reader/writer 
 * Subpixel ClearType support 
 * Better RTF handling, e.g., multilevel lists 
 * URL tooltips 
 * Many bug/minor-request fixes 
 * Improved ink support, especially for OneNote 
 * Advanced East Asian typography 
 * Initial PTS integration, including object tight wrap 
 * Infrastructure for math, ruby, warichu, tatenakayoko 
 * Text trackers and blobs 
 * 
 * 
 * ### RichEdit 5.1 
 * Third Windows CE release. Used by Pocket Word 
 * Various UI and RTF enhancements 
 * 
 * 
 * ### RichEdit 6.0 Additions 
 * High-quality editing & display of math 
 * Formula autobuildup 
 * Create and support math linear format 
 * More list numbering options 
 * Simple "visi" mode 
 * URL improvements
 * Multistory: high-perf cut/copy/paste, rich scratchpads, WP infrastructure 
 * Text Object Model 2 
 * Display enhancements, e.g., word underline, horizontal scaling 
 * Table UI adds, e.g., column resizing 
 * OfficeArt/PowerPoint enhancements 
 * Overlapping lines, drop caps & other ePeriodicals improvements 
 * Device independent layout 
 * Virtualized OS: "hDC" is totally opaque 
 * Multiple columns
 * Myriad security fixes
 * 
 * 
 * ### RichEdit 5.0 ###
 * It is distributed with Microsoft Office 2003. It is located in
 * X:\Program Files\Common Files\Microsoft Shared\OFFICE11 (replace X: with correspond letter)
 * 
 * 
 * ### RichEdit 6.0 ###
 * It is distributed with Microsoft Office 2007. It is located in
 * X:\Program Files\Common Files\Microsoft Shared\OFFICE12 (replace X: with correspond letter)
 * 
 * 
 * Versions 5.0 and 6.0 has the same DLL name - RICHED20.DLL.
 * 
 * 
 * REMEMBER!!! There is no documentation. Using o
 * 
 */


using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Drawing.Printing;
using System.ComponentModel;
using System.Security.Permissions;

/// <summary>
/// This ExtendedRichTextBox is based on .NET Framework and also uses WinAPI
/// to extend its functionality. It also contains wrappers for TOM 
/// (Text Object Model) and for OLE (Object Linking and Embedding - this
/// code was written by Oscar Londoño, go to 
/// 
/// http://www.codeproject.com/KB/edit/MyExtRichTextBox.aspx 
/// 
/// for more details)
/// </summary>
[Serializable()]
public class ExtendedRichTextBox : RichTextBox
{
    #region CONSTRUCTOR
    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr LoadLibrary(string libname);

    private static IntPtr RichEditModuleHandle;
    //Libraries
    private const string RichEditDllV3 = "RichEd20.dll";
    private const string RichEditDllV41 = "Msftedit.dll";
    /*You can also specify*/


    //Class names
    private const string RichEditClassV3A = "RichEdit20A";
    private const string RichEditClassV3W = "RichEdit20W";
    private const string RichEditClassV41W = "RICHEDIT50W";
    private const string RichEditClass50 = "RichEdit50W"; //Office 2003 required
    private const string RichEditClass60 = "RichEdit60W"; //Office 2007 required
   
    protected override CreateParams CreateParams
    {
        [SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
        get
        {
            //Check the existence of the library
            RichEditModuleHandle = LoadLibrary(RichEditDllV41);
            if (RichEditModuleHandle == IntPtr.Zero)
            {
                return base.CreateParams;
            }
            
            CreateParams par = base.CreateParams;
            //Assign correspond class name
            par.ClassName = RichEditClassV41W;            
            return par;            
        }
    }

    private string dropPath = "";    
    public ExtendedRichTextBox()
    {
        this.EnableAutoDragDrop = false;
        SetLayoutType(LayoutModes.WYSIWYG);
    }
    #endregion

    #region PARAFORMAT MASKS
    private const uint PFN_BULLET = 0x0001;

    private const uint PFM_STARTINDENT = 0x00000001;
    private const uint PFM_RIGHTINDENT = 0x00000002;
    private const uint PFM_OFFSET = 0x00000004;
    private const uint PFM_ALIGNMENT = 0x00000008;
    private const uint PFM_TABSTOPS = 0x00000010;
    private const uint PFM_NUMBERING = 0x00000020;
    private const uint PFM_OFFSETINDENT = 0x80000000;

    private const uint PFM_SPACEBEFORE = 0x00000040;
    private const uint PFM_SPACEAFTER = 0x00000080;
    private const uint PFM_LINESPACING = 0x00000100;
    private const uint PFM_STYLE = 0x00000400;
    private const uint PFM_BORDER = 0x00000800;	            /* (*)	*/
    private const uint PFM_SHADING = 0x00001000;	        /* (*)	*/
    private const uint PFM_NUMBERINGSTYLE = 0x00002000;	    /* (*)	*/
    private const uint PFM_NUMBERINGTAB = 0x00004000;	    /* (*)	*/
    private const uint PFM_NUMBERINGSTART = 0x00008000;	    /* (*)	*/

    private const uint PFM_DIR = 0x00010000;
    private const uint PFM_RTLPARA = 0x00010000;	        /* (Version 1.0 flag) */
    private const uint PFM_KEEP = 0x00020000;	            /* (*)	*/
    private const uint PFM_KEEPNEXT = 0x00040000;	        /* (*)	*/
    private const uint PFM_PAGEBREAKBEFORE = 0x00080000;	/* (*)	*/
    private const uint PFM_NOLINENUMBER = 0x00100000;	    /* (*)	*/
    private const uint PFM_NOWIDOWCONTROL = 0x00200000;	    /* (*)	*/
    private const uint PFM_DONOTHYPHEN = 0x00400000;	    /* (*)	*/
    private const uint PFM_SIDEBYSIDE = 0x00800000;	        /* (*)	*/

    private const uint PFM_TABLE = 0xc0000000;	            /* (*)	*/

    #endregion

    #region CHARFORMAT MASKS
    private const uint CFM_BOLD = 0x00000001;
    private const uint CFM_ITALIC = 0x00000002;
    private const uint CFM_UNDERLINE = 0x00000004;
    private const uint CFM_STRIKEOUT = 0x00000008;
    private const uint CFM_PROTECTED = 0x00000010;
    private const uint CFM_LINK = 0x00000020;		/* This is for hyperlinks in document */
    private const uint CFM_SIZE = 0x80000000;
    private const uint CFM_COLOR = 0x40000000;
    private const uint CFM_FACE = 0x20000000;
    private const uint CFM_OFFSET = 0x10000000;
    private const uint CFM_CHARSET = 0x08000000;

    private const uint CFM_FONTSTYLE_EFFECTS = (CFM_BOLD | CFM_ITALIC | CFM_UNDERLINE | CFM_STRIKEOUT | CFM_LINK);
    private const uint CFM_FORMATTING_EFFECTS = (CFM_BOLD | CFM_ITALIC | CFM_UNDERLINE | CFM_STRIKEOUT | CFM_LINK | CFM_COLOR);

    /* (*) */     // this means supported but not displayed.

    private const uint CFM_SMALLCAPS = 0x0040;			/* (*) */
    private const uint CFM_ALLCAPS = 0x0080;			/* (*) */
    private const uint CFM_HIDDEN = 0x0100;			    /* (*) */
    private const uint CFM_OUTLINE = 0x0200;			/* (*) */
    private const uint CFM_SHADOW = 0x0400;			    /* (*) */
    private const uint CFM_EMBOSS = 0x0800;			    /* (*) */
    private const uint CFM_IMPRINT = 0x1000;			/* (*) */
    private const uint CFM_DISABLED = 0x2000;
    private const uint CFM_REVISED = 0x4000;

    private const uint CFM_BACKCOLOR = 0x04000000;
    private const uint CFM_LCID = 0x02000000;
    private const uint CFM_UNDERLINETYPE = 0x00800000;	/* (*) */
    private const uint CFM_WEIGHT = 0x00400000;
    private const uint CFM_SPACING = 0x00200000;		/* (*) */
    private const uint CFM_KERNING = 0x00100000;		/* (*) */
    private const uint CFM_STYLE = 0x00080000;		    /* (*) */
    private const uint CFM_ANIMATION = 0x00040000;		/* (*) */
    private const uint CFM_REVAUTHOR = 0x00008000;
    #endregion

    #region PARAFORMAT EFFECTS
    private const uint PFE_RTLPARA = (PFM_DIR >> 16);
    private const uint PFE_RTLPAR = (PFM_RTLPARA >> 16);	                /* (Version 1.0 flag) */
    private const uint PFE_KEEP = (PFM_KEEP >> 16);	                        /* (*) */
    private const uint PFE_KEEPNEXT = (PFM_KEEPNEXT >> 16);	                /* (*) */
    private const uint PFE_PAGEBREAKBEFORE = (PFM_PAGEBREAKBEFORE >> 16);	/* (*) */
    private const uint PFE_NOLINENUMBER = (PFM_NOLINENUMBER >> 16);	        /* (*) */
    private const uint PFE_NOWIDOWCONTROL = (PFM_NOWIDOWCONTROL >> 16);	    /* (*) */
    private const uint PFE_DONOTHYPHEN = (PFM_DONOTHYPHEN >> 16);	        /* (*) */
    private const uint PFE_SIDEBYSIDE = (PFM_SIDEBYSIDE >> 16);	            /* (*) */

    private const uint PFE_TABLEROW = 0xc000;		    /* These 3 options are mutually	*/
    private const uint PFE_TABLECELLEND = 0x8000;		/* exclusive and each imply	    */
    private const uint PFE_TABLECELL = 0x4000;		    /* hat para is part of a table  */
    #endregion

    #region CHARFORMAT EFFECTS
    private const uint CFE_BOLD = 0x0001;
    private const uint CFE_ITALIC = 0x0002;
    private const uint CFE_UNDERLINE = 0x0004;
    private const uint CFE_STRIKEOUT = 0x0008;
    private const uint CFE_LINK = 0x0020;

    private const uint CFE_SUBSCRIPT = 0x00010000;		/* Superscript and subscript are */
    private const uint CFE_SUPERSCRIPT = 0x00020000;	/*  mutually exclusive */

    private const uint CFE_AUTOCOLOR = 0x40000000;
    #endregion

    #region FORMAT_RANGE_CONSTANTS
    private const int SCF_SELECTION = 0x0001;
    private const int SCF_WORD = 0x0002;
    private const int SCF_DEFAULT = 0x0000;	// set the default charformat or paraformat
    private const int SCF_ALL = 0x0004;		// not valid with SCF_SELECTION or SCF_WORD
    private const int SCF_USEUIRULES = 0x0008;
    #endregion

    #region TEXT OBJECT MODEL
    public class WinAPI
    {
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hwnd, int Msg, IntPtr wParam, [MarshalAs(UnmanagedType.IUnknown)] out object lParam);
    }

    public enum tomConstants : int
    {
        tomFalse = 0,
        tomTrue = -1,
        tomUndefined = -9999999,
        tomToggle = -9999998,
        tomAutoColor = -9999997,
        tomDefault = -9999996,
        tomBackward = -1073741823,
        tomForward = 1073741823,
        tomMove = 0,
        tomExtend = 1,
        tomNoSelection = 0,
        tomSelectionIP = 1,
        tomSelectionNormal = 2,
        tomSelectionFrame = 3,
        tomSelectionColumn = 4,
        tomSelectionRow = 5,
        tomSelectionBlock = 6,
        tomSelectionInlineShape = 7,
        tomSelectionShape = 8,
        tomSelStartActive = 1,
        tomSelAtEOL = 2,
        tomSelOvertype = 4,
        tomSelActive = 8,
        tomSelReplace = 16,
        tomEnd = 0,
        tomStart = 32,
        tomCollapseEnd = 0,
        tomCollapseStart = 1,
        tomNone = 0,
        tomSingle = 1,
        tomWords = 2,
        tomDouble = 3,
        tomDotted = 4,
        tomLineSpaceSingle = 0,
        tomLineSpace1pt5 = 1,
        tomLineSpaceDouble = 2,
        tomLineSpaceAtLeast = 3,
        tomLineSpaceExactly = 4,
        tomLineSpaceMultiple = 5,
        tomAlignLeft = 0,
        tomAlignCenter = 1,
        tomAlignRight = 2,
        tomAlignJustify = 3,
        tomAlignDecimal = 3,
        tomAlignBar = 4,
        tomSpaces = 0,
        tomDots = 1,
        tomDashes = 2,
        tomLines = 3,
        tomTabBack = -3,
        tomTabNext = -2,
        tomTabHere = -1,
        tomListNone = 0,
        tomListBullet = 1,
        tomListNumberAsArabic = 2,
        tomListNumberAsLCLetter = 3,
        tomListNumberAsUCLetter = 4,
        tomListNumberAsLCRoman = 5,
        tomListNumberAsUCRoman = 6,
        tomListNumberAsSequence = 7,
        tomListParentheses = 65536,
        tomListPeriod = 131072,
        tomListPlain = 196608,
        tomCharacter = 1,
        tomWord = 2,
        tomSentence = 3,
        tomParagraph = 4,
        tomLine = 5,
        tomStory = 6,
        tomScreen = 7,
        tomSection = 8,
        tomColumn = 9,
        tomRow = 10,
        tomWindow = 11,
        tomCell = 12,
        tomCharFormat = 13,
        tomParaFormat = 14,
        tomTable = 15,
        tomObject = 16,
        tomMatchWord = 2,
        tomMatchCase = 4,
        tomMatchPattern = 8,
        tomUnknownStory = 0,
        tomMainTextStory = 1,
        tomFootnotesStory = 2,
        tomEndnotesStory = 3,
        tomCommentsStory = 4,
        tomTextFrameStory = 5,
        tomEvenPagesHeaderStory = 6,
        tomPrimaryHeaderStory = 7,
        tomEvenPagesFooterStory = 8,
        tomPrimaryFooterStory = 9,
        tomFirstPageHeaderStory = 10,
        tomFirstPageFooterStory = 11,
        tomNoAnimation = 0,
        tomLasVegasLights = 1,
        tomBlinkingBackground = 2,
        tomSparkleText = 3,
        tomMarchingBlackAnts = 4,
        tomMarchingRedAnts = 5,
        tomShimmer = 6,
        tomWipeDown = 7,
        tomWipeRight = 8,
        tomAnimationMax = 8,
        tomLowerCase = 0,
        tomUpperCase = 1,
        tomTitleCase = 2,
        tomSentenceCase = 4,
        tomToggleCase = 5,
        tomReadOnly = 256,
        tomShareDenyRead = 512,
        tomShareDenyWrite = 1024,
        tomPasteFile = 4096,
        tomCreateNew = 16,
        tomCreateAlways = 32,
        tomOpenExisting = 48,
        tomOpenAlways = 64,
        tomTruncateExisting = 80,
        tomRTF = 1,
        tomText = 2,
        tomHTML = 3,
        tomWordDocument = 4
    }

    public interface ITextDocument //{8CC497C0-A1DF-11CE-8098-00AA0047BE5D}
    {
        String Name { get; }

        ITextSelection Selection { get; }
        ITextStoryRanges StoryRanges { get; }
        ITextRange Range(long cp1, long cp2);
        ITextRange RangeFromPoint(long x, long y);

        Int32 StoryCount { get; }

        Int32 Saved { get; set; }

        Single DefaultTabStop { get; set; }

        void New();

        void Open([MarshalAs(UnmanagedType.AsAny)] string pVar, Int32 Flags, Int32 CodePage);
        void Save([MarshalAs(UnmanagedType.AsAny)] string pVar, Int32 Flags, Int32 CodePage);

        Int32 Freeze();
        Int32 Unfreeze();

        void BeginEditCollection();
        void EndEditCollection();

        Int32 Undo(Int32 Count);
        Int32 Redo(Int32 Count);



    }
    public interface ITextFont //{8CC497C3-A1DF-11CE-8098-00AA0047BE5D}
    {
        ITextFont Duplicate { get; }
        Int32 CanChange();
        Int32 IsEqual(ITextFont pFont);
        void Reset(Int32 Value);
        Int32 Style { get; set; }
        Int32 AllCaps { get; set; }
        Int32 Animation { get; set; }
        Int32 BackColor { get; set; }
        Int32 Bold { get; set; }
        Int32 Emboss { get; set; }
        Int32 ForeColor { get; set; }
        Int32 Hidden { get; set; }
        Int32 Engrave { get; set; }
        Int32 Italic { get; set; }
        Single Kerning { get; set; }
        Int32 LanguageID { get; set; }
        String Name { get; set; }
        Int32 Outline { get; set; }
        Single Position { get; set; }
        Int32 Protected { get; set; }
        Int32 Shadow { get; set; }
        Single Size { get; set; }
        Int32 SmallCaps { get; set; }
        Single Spacing { get; set; }
        Int32 StrikeTrough { get; set; }
        Int32 Subscript { get; set; }
        Int32 Superscript { get; set; }
        Int32 Underline { get; set; }
        Int32 Weight { get; set; }
    }
    public interface ITextRange //{8CC497C2-A1DF-11CE-8098-00AA0047BE5D}
    {
        string Text { get; set; }
        Int32 Char { get; set; }
        ITextRange Dublicate { get; }
        ITextRange FormattedText { get; set; }
        Int32 Start { get; set; }
        Int32 End { get; set; }
        ITextFont Font { get; set; }
        ITextPara Para { get; set; }
        Int32 StoryLength { get; }
        Int32 StoryType { get; }
        void Collapse(Int32 bStart);
        Int32 Expand(Int32 Unit);
        Int32 GetIndex(Int32 Unit);
        void SetIndex(Int32 Unit, Int32 Index, Int32 Extend);
        void SetRange(Int32 cpActive, Int32 cpOther);
        Int32 InRange(ITextRange pRange);
        Int32 InStory(ITextRange pRange);
        Int32 IsEqual(ITextRange pRange);
        void Select();
        Int32 StartOf(Int32 Unit, Int32 Extend);
        Int32 EndOf(Int32 Unit, Int32 Extend);
        Int32 Move(Int32 Unit, Int32 Count);
        Int32 MoveStart(Int32 Unit, Int32 Count);
        Int32 MoveEnd(Int32 Unit, Int32 Count);
        Int32 MoveWhile([MarshalAs(UnmanagedType.AsAny)]Int32 Cset, Int32 Count);
        Int32 MoveStartWhile([MarshalAs(UnmanagedType.AsAny)]Int32 Cset, Int32 Count);
        Int32 MoveEndWhile([MarshalAs(UnmanagedType.AsAny)]Int32 Cset, Int32 Count);
        Int32 MoveUntil([MarshalAs(UnmanagedType.AsAny)]Int32 Cset, Int32 Count);
        Int32 MoveStartUntil([MarshalAs(UnmanagedType.AsAny)]Int32 Cset, Int32 Count);
        Int32 MoveEndUntil([MarshalAs(UnmanagedType.AsAny)]Int32 Cset, Int32 Count);
        Int32 FindText(string bstr, Int32 cch, Int32 Flags);
        Int32 FindTextStart(string bstr, Int32 cch, Int32 Flags);
        Int32 FindTextEnd(string bstr, Int32 cch, Int32 Flags);
        Int32 Delete(Int32 Unit, Int32 Count);
        void Cut([MarshalAs(UnmanagedType.AsAny)] Int32 pVar);
        void Copy([MarshalAs(UnmanagedType.AsAny)] Int32 pVar);
        void Paste([MarshalAs(UnmanagedType.AsAny)] Int32 pVar);
        Int32 CanPaste([MarshalAs(UnmanagedType.AsAny)] Int32 pVar, Int32 Format);
        Int32 CanEdit();
        void ChangeCase(Int32 Type);
        void GetPoint(Int32 Type, Int32 px, Int32 py);
        void SetPoint(Int32 x, Int32 px, Int32 y, Int32 Type, Int32 Extend);
        void ScrollIntoView(Int32 Value);
        Object GetEmbeddedObject();
    }
    public interface ITextPara //{8CC497C4-A1DF-11CE-8098-00AA0047BE5D}
    {
        ITextPara Duplicate { get; set; }
        Int32 CanChange();
        Int32 IsEqual(ITextPara pPara);
        void Reset(Int32 Value);
        Int32 Style { get; set; }
        Int32 Alignment { get; set; }
        Int32 Hyphenation { get; set; }
        Single FirstLineIndent { get; }
        Int32 KeepTogether { get; set; }
        Int32 KeppWithNext { get; set; }
        Single LeftIndent { get; set; }
        Int32 LineSpacingRule { get; set; }
        Int32 ListAlignment { get; set; }
        Int32 ListLevelIndex { get; set; }
        Int32 ListStart { get; set; }
        Single ListTab { get; set; }
        Int32 ListType { get; set; }
        Int32 NoLineNumber { get; set; }
        Int32 PageBreakBefore { get; set; }
        Single RightIndent { get; set; }
        void SetIndents(Single StartIndent, Single LeftIndent, Single RightIndent);
        void SetLineSpacing(Int32 LineSpacingRule, Single LineSpacing);
        Single SpaceAfter { get; set; }
        Single SpaceBefore { get; set; }
        Int32 WindowControl { get; set; }
        Int32 TabCount { get; }
        void AddTab(Single tbPos, Int32 tbAlign, Int32 tbLeader);
        void ClearAllTabs();
        void DeleteTab(Single tbPos);
        void GetTab(Int32 iTab, float ptbPos, Int32 ptbAlign, Int32 ptbLeader);
    }
    public interface ITextSelection //{8CC497C1-A1DF-11CE-8098-00AA0047BE5D}
    {
        string Text { get; set; }
        Int32 Char { get; set; }
        ITextRange Duplicate { get; }
        ITextRange FormattedText { get; set; }
        Int32 Start { get; set; }
        Int32 End { get; set; }
        ITextFont Font { get; set; }
        ITextPara Para { get; set; }
        Int32 StoryLength { get; }
        Int32 StoryType { get; }
        void Collapse(Int32 bStart);
        Int32 Expand(Int32 Unit);
        Int32 GetIndex(Int32 Unit);
        void SetIndex(Int32 Unit, Int32 Index, Int32 Extend);
        void SetRange(Int32 cpActive, Int32 cpOther);
        Int32 InRange(ITextRange pRange);
        Int32 InStory(ITextRange pRange);
        Int32 IsEqual(ITextRange pRange);
        void Select();
        Int32 StartOf(Int32 Unit, Int32 Extend);
        Int32 EndOf(Int32 Unit, Int32 Extend);
        Int32 Move(Int32 Unit, Int32 Extend);
        Int32 MoveStart(Int32 Unit, Int32 Extend);
        Int32 MoveEnd(Int32 Unit, Int32 Extend);
        Int32 MoveWhile([MarshalAs(UnmanagedType.AsAny)]Int32 Cset, Int32 Count);
        Int32 MoveStartWhile([MarshalAs(UnmanagedType.AsAny)]Int32 Cset, Int32 Count);
        Int32 MoveEndWhile([MarshalAs(UnmanagedType.AsAny)]Int32 Cset, Int32 Count);
        Int32 MoveUntil([MarshalAs(UnmanagedType.AsAny)]Int32 Cset, Int32 Count);
        Int32 MoveStartUntil([MarshalAs(UnmanagedType.AsAny)]Int32 Cset, Int32 Count);
        Int32 MoveEndUntil([MarshalAs(UnmanagedType.AsAny)]Int32 Cset, Int32 Count);
        Int32 FindText(string bstr, Int32 cch, Int32 Flags);
        Int32 FindTextStart(string bstr, Int32 cch, Int32 Flags);
        Int32 FindTextEnd(string bstr, Int32 cch, Int32 Flags);
        Int32 Delete(Int32 Unit, Int32 Count);
        void Cut([MarshalAs(UnmanagedType.AsAny)] Int32 pVar);
        void Copy([MarshalAs(UnmanagedType.AsAny)] Int32 pVar);
        void Paste([MarshalAs(UnmanagedType.AsAny)] Int32 pVar);
        Int32 CanPaste([MarshalAs(UnmanagedType.AsAny)] Int32 pVar, Int32 Format);
        Int32 CanEdit();
        void ChangeCase(Int32 Type);
        void GetPoint(Int32 Type, Int32 px, Int32 py);
        void SetPoint(Int32 x, Int32 px, Int32 y, Int32 Type, Int32 Extend);
        void ScrollIntoView(Int32 Value);
        Object GetEmbeddedObject();
        Int32 Flags { get; set; }
        Int32 Type { get; }
        Int32 MoveLeft(Int32 Unit, Int32 Count, Int32 Extend);
        Int32 MoveRight(Int32 Unit, Int32 Count, Int32 Extend);
        Int32 MoveUp(Int32 Unit, Int32 Count, Int32 Extend);
        Int32 MoveDown(Int32 Unit, Int32 Count, Int32 Extend);
        Int32 HomeKey(Int32 Unit, Int32 Extend);
        Int32 EndKey(Int32 Unit, Int32 Extend);
        void TypeText(string bstr);
    }
    public interface ITextStoryRanges //{8CC497C5-A1DF-11CE-8098-00AA0047BE5D}
    {
        ITextRange Item(Int32 Index);
        System.Collections.IEnumerator GetEnumerator();
        Int32 Count { get; }
    }
    #endregion

    #region CONTROL LAYOUT

    public const int EM_SETTARGETDEVICE = (WM_USER + 72);
    public enum LayoutModes : int
    {
        Default = 0,
        WordWrap = 1,
        /// <summary>
        /// What You See Is What You Get
        /// </summary>
        WYSIWYG = 2
    }

    [DllImport("user32.dll")]
    public static extern long SendMessageLong(long hwnd, long Msg, long wParam, long lParam);

    public void SetLayoutType(LayoutModes eViewMode)
    {
        PrintDocument pd = new PrintDocument();
        PrinterSettings ps = new PrinterSettings();
        
        //int* y = (int*)500;

        switch (eViewMode)
        {
            case LayoutModes.WYSIWYG:
                //SendMessage(this.Handle, EM_SETTARGETDEVICE, ps.GetHdevmode(), new IntPtr(y));
                break;
            case LayoutModes.WordWrap:
                SendMessage(new HandleRef(this, this.Handle), EM_SETTARGETDEVICE, 0, 0);
                break;
            case LayoutModes.Default:
                SendMessage(new HandleRef(this, this.Handle), EM_SETTARGETDEVICE, 0, 1);
                break;
        }
    }
    #endregion

    #region FORMATTING AND PRINTING

    #region Windows API

    private const int WM_SETREDRAW = 0x0B;
    private const int EM_SETEVENTMASK = 0x0431;
    private const int EM_SETCHARFORMAT = 0x0444;
    private const int EM_GETCHARFORMAT = 0x043A;
    private const int EM_GETPARAFORMAT = 0x043D;
    private const int EM_SETPARAFORMAT = 0x0447;
    private const int EM_SETTYPOGRAPHYOPTIONS = 0x04CA;
    //private const int CFM_UNDERLINETYPE = 0x800000;
    //private const int CFM_BACKCOLOR = 0x4000000;
    private const int CFE_AUTOBACKCOLOR = 0x4000000;
    //private const int PFM_ALIGNMENT = 0x08;
    private const int TO_ADVANCEDTYPOGRAPHY = 0x01;

    [DllImport("user32", CharSet = CharSet.Auto)]
    private static extern int SendMessage(HandleRef hWnd, int msg, int wParam, int lParam);

    [DllImport("user32", CharSet = CharSet.Auto)]
    private static extern int SendMessage(HandleRef hWnd, int msg, int wParam, ref CHARFORMAT2 lParam);

    [DllImport("user32", CharSet = CharSet.Auto)]
    private static extern int SendMessage(HandleRef hWnd, int msg, int wParam, ref IRichEditOleCallback lParam);

    [DllImport("user32", CharSet = CharSet.Auto)]
    private static extern int SendMessage(HandleRef hWnd, int msg, int wParam, ref PARAFORMAT2 lParam);

    [DllImport("USER32", EntryPoint = "SendMessageA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

    /// <summary> 
    /// Contains information about character formatting in a rich edit control. 
    /// </summary> 
    /// <remarks><see cref="CHARFORMAT"/> works with all Rich Edit versions.</remarks> 
    [StructLayout(LayoutKind.Sequential)]
    private struct CHARFORMAT
    {
        public int cbSize;
        public uint dwMask;
        public uint dwEffects;
        public int yHeight;
        public int yOffset;
        public int crTextColor;
        public byte bCharSet;
        public byte bPitchAndFamily;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public char[] szFaceName;
    }

    /// <summary> 
    /// Contains information about character formatting in a rich edit control. 
    /// </summary> 
    /// <remarks><see cref="CHARFORMAT2"/> requires Rich Edit 2.0.</remarks> 
    [StructLayout(LayoutKind.Sequential)]
    private struct CHARFORMAT2
    {
        public int cbSize;
        public uint dwMask;
        public uint dwEffects;
        public int yHeight;
        public int yOffset;
        public int crTextColor;
        public byte bCharSet;
        public byte bPitchAndFamily;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public char[] szFaceName;
        public short wWeight;
        public short sSpacing;
        public int crBackColor;
        public int LCID;
        public uint dwReserved;
        public short sStyle;
        public short wKerning;
        public byte bUnderlineType;
        public byte bAnimation;
        public byte bRevAuthor;
    }

    /// <summary> 
    /// Contains information about paragraph formatting in a rich edit control. 
    /// </summary> 
    /// <remarks><see cref="PARAFORMAT"/> works with all Rich Edit versions.</remarks> 
    [StructLayout(LayoutKind.Sequential)]
    private struct PARAFORMAT
    {
        public int cbSize;
        public uint dwMask;
        public short wNumbering;
        public short wReserved;
        public int dxStartIndent;
        public int dxRightIndent;
        public int dxOffset;
        public short wAlignment;
        public short cTabCount;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public int[] rgxTabs;
    }

    /// <summary> 
    /// Contains information about paragraph formatting in a rich edit control. 
    /// </summary> 
    /// <remarks><see cref="PARAFORMAT2"/> requires Rich Edit 2.0.</remarks> 
    [StructLayout(LayoutKind.Sequential)]
    private struct PARAFORMAT2
    {
        public int cbSize;
        public uint dwMask;
        public short wNumbering;
        public short wReserved;
        public int dxStartIndent;
        public int dxRightIndent;
        public int dxOffset;
        public short wAlignment;
        public short cTabCount;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public int[] rgxTabs;
        public int dySpaceBefore;
        public int dySpaceAfter;
        public int dyLineSpacing;
        public short sStyle;
        public byte bLineSpacingRule;
        public byte bOutlineLevel;
        public short wShadingWeight;
        public short wShadingStyle;
        public short wNumberingStart;
        public short wNumberingStyle;
        public short wNumberingTab;
        public short wBorderSpace;
        public short wBorderWidth;
        public short wBorders;
    }

    #endregion
    #region Method: OnHandleCreated

    /// <summary> 
    /// Raises the <see cref="HandleCreated"/> event. 
    /// </summary> 
    /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param> 
    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);

        // Enable support for justification 
        SendMessage(new HandleRef(this, Handle), EM_SETTYPOGRAPHYOPTIONS, TO_ADVANCEDTYPOGRAPHY, TO_ADVANCEDTYPOGRAPHY);
    }

    #endregion

    #region VARIABLES, CONSTANTS AND ENUMS
    /// <summary> 
    /// Specifies horizontal alignment for a segment of rich text. 
    /// </summary> 
    public enum RichTextAlign
    {
        /// <summary> 
        /// The text alignment is unknown. 
        /// </summary> 
        Unknown = 0,

        /// <summary> 
        /// The text is aligned to the left. 
        /// </summary> 
        Left = 1,

        /// <summary> 
        /// The text is aligned to the right. 
        /// </summary> 
        Right = 2,

        /// <summary> 
        /// The text is aligned in the center. 
        /// </summary> 
        Center = 3,

        /// <summary> 
        /// The text is justified. 
        /// </summary> 
        Justify = 4
    }
    /// <summary> 
    /// Specifies the underline styles for a segment of rich text. 
    /// </summary> 
    public enum UnderlineStyle
    {
        /// <summary> 
        /// No underlining. 
        /// </summary> 
        None = 0,

        /// <summary> 
        /// Single-line solid underline. 
        /// </summary> 
        Normal = 1,

        /// <summary> 
        /// Single-line underline broken between words. 
        /// </summary> 
        Word = 2,

        /// <summary> 
        /// Double-line underline. 
        /// </summary> 
        Double = 3,

        /// <summary> 
        /// 'Dotted' pattern underline. 
        /// </summary> 
        Dotted = 4,

        /// <summary> 
        /// 'Dash' pattern underline. 
        /// </summary> 
        Dash = 5,

        /// <summary> 
        /// 'Dash-dot' pattern underline. 
        /// </summary> 
        DashDot = 6,

        /// <summary> 
        /// 'Dash-dot-dot' pattern underline. 
        /// </summary> 
        DashDotDot = 7,

        /// <summary> 
        /// Single-line wave style underline. 
        /// </summary> 
        Wave = 8,

        /// <summary> 
        /// Single-line solid underline with extra thickness. 
        /// </summary> 
        Thick = 9,

        /// <summary> 
        /// Single-line solid underline with less thickness. 
        /// </summary> 
        HairLine = 10,

        /// <summary> 
        /// Double-line wave style underline. 
        /// </summary> 
        DoubleWave = 11,

        /// <summary> 
        /// Single-line wave style underline with extra thickness. 
        /// </summary> 
        HeavyWave = 12,

        /// <summary> 
        /// 'Long Dash' pattern underline. 
        /// </summary> 
        LongDash = 13,

        /// <summary> 
        /// 'Dash' pattern underline with extra thickness. 
        /// </summary> 
        ThickDash = 14,

        /// <summary> 
        /// 'Dash-dot' pattern underline with extra thickness. 
        /// </summary> 
        ThickDashDot = 15,

        /// <summary> 
        /// 'Dash-dot-dot' pattern underline with extra thickness. 
        /// </summary> 
        ThickDashDotDot = 16,

        /// <summary> 
        /// 'Dotted' pattern underline with extra thickness. 
        /// </summary> 
        ThickDotted = 17,

        /// <summary> 
        /// 'Long Dash' pattern underline with extra thickness. 
        /// </summary> 
        ThickLongDash = 18
    }
    /// <summary> 
    /// Specifies the color of underline for a segment of rich text. 
    /// </summary> 
    public enum UnderlineColor
    {
        /// <summary> 
        /// No specific underline color specified. 
        /// </summary> 
        None = -1,

        /// <summary> 
        /// Black. 
        /// </summary> 
        Black = 0x00,

        /// <summary> 
        /// Blue. 
        /// </summary> 
        Blue = 0x10,

        /// <summary> 
        /// Cyan. 
        /// </summary> 
        Cyan = 0x20,

        /// <summary> 
        /// LimeGreen. 
        /// </summary> 
        LimeGreen = 0x30,

        /// <summary> 
        /// Magenta. 
        /// </summary> 
        Magenta = 0x40,

        /// <summary> 
        /// Red. 
        /// </summary> 
        Red = 0x50,

        /// <summary> 
        /// Yellow. 
        /// </summary> 
        Yellow = 0x60,

        /// <summary> 
        /// White. 
        /// </summary> 
        White = 0x70,

        /// <summary> 
        /// DarkBlue. 
        /// </summary> 
        DarkBlue = 0x80,

        /// <summary> 
        /// DarkCyan. 
        /// </summary> 
        DarkCyan = 0x90,

        /// <summary> 
        /// Green. 
        /// </summary> 
        Green = 0xA0,

        /// <summary> 
        /// DarkMagenta. 
        /// </summary> 
        DarkMagenta = 0xB0,

        /// <summary> 
        /// Brown. 
        /// </summary> 
        Brown = 0xC0,

        /// <summary> 
        /// OliveGreen. 
        /// </summary> 
        OliveGreen = 0xD0,

        /// <summary> 
        /// DarkGray. 
        /// </summary> 
        DarkGray = 0xE0,

        /// <summary> 
        /// Gray. 
        /// </summary> 
        Gray = 0xF0
    }

    #region Property: SelectionUnderlineStyle

    /// <summary> 
    /// Gets or sets the underline style to apply to the current selection or insertion point. 
    /// </summary> 
    /// <value>A <see cref="UnderlineStyle"/> that represents the underline style to 
    /// apply to the current text selection or to text entered after the insertion point.</value> 
    [Browsable(false),
    DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public UnderlineStyle SelectionUnderlineStyle
    {
        get
        {
            CHARFORMAT2 fmt = new CHARFORMAT2();
            fmt.cbSize = Marshal.SizeOf(fmt);

            // Get the underline style 
            SendMessage(new HandleRef(this, Handle), EM_GETCHARFORMAT, SCF_SELECTION, ref fmt);
            if ((fmt.dwMask & CFM_UNDERLINETYPE) == 0)
            {
                return UnderlineStyle.None;
            }
            else
            {
                byte style = (byte)(fmt.bUnderlineType & 0x0F);
                return (UnderlineStyle)style;
            }
        }
        set
        {
            // Ensure we don't alter the color 
            UnderlineColor color = SelectionUnderlineColor;

            // Ensure we don't show it if it shouldn't be shown 
            if (value == UnderlineStyle.None)
                color = UnderlineColor.Black;

            // Set the underline type 
            CHARFORMAT2 fmt = new CHARFORMAT2();
            fmt.cbSize = Marshal.SizeOf(fmt);
            fmt.dwMask = CFM_UNDERLINETYPE;
            fmt.bUnderlineType = (byte)((byte)value | (byte)color);
            SendMessage(new HandleRef(this, Handle), EM_SETCHARFORMAT, SCF_SELECTION, ref fmt);
        }
    }

    #endregion
    #region Property: SelectionUnderlineColor

    /// <summary> 
    /// Gets or sets the underline color to apply to the current selection or insertion point. 
    /// </summary> 
    /// <value>A <see cref="UnderlineColor"/> that represents the underline color to 
    /// apply to the current text selection or to text entered after the insertion point.</value> 
    [Browsable(false),
    DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public UnderlineColor SelectionUnderlineColor
    {
        get
        {
            CHARFORMAT2 fmt = new CHARFORMAT2();
            fmt.cbSize = Marshal.SizeOf(fmt);

            // Get the underline color 
            SendMessage(new HandleRef(this, Handle), EM_GETCHARFORMAT, SCF_SELECTION, ref fmt);
            if ((fmt.dwMask & CFM_UNDERLINETYPE) == 0)
            {
                return UnderlineColor.None;
            }
            else
            {
                byte style = (byte)(fmt.bUnderlineType & 0xF0);
                return (UnderlineColor)style;
            }
        }
        set
        {
            // If the an underline color of "None" is specified, remove underline effect 
            if (value == UnderlineColor.None)
            {
                SelectionUnderlineStyle = UnderlineStyle.None;
            }
            else
            {
                // Ensure we don't alter the style 
                UnderlineStyle style = SelectionUnderlineStyle;

                // Ensure we don't show it if it shouldn't be shown 
                if (style == UnderlineStyle.None)
                    value = UnderlineColor.Black;

                // Set the underline color 
                CHARFORMAT2 fmt = new CHARFORMAT2();
                fmt.cbSize = Marshal.SizeOf(fmt);
                fmt.dwMask = CFM_UNDERLINETYPE;
                fmt.bUnderlineType = (byte)((byte)style | (byte)value);
                SendMessage(new HandleRef(this, Handle), EM_SETCHARFORMAT, SCF_SELECTION, ref fmt);
            }
        }
    }

    #endregion
    #region Properties: Selection Colors

    /// <summary> 
    /// Gets or sets the background color to apply to the 
    /// current selection or insertion point. 
    /// </summary> 
    /// <value>A <see cref="Color"/> that represents the background color to 
    /// apply to the current text selection or to text entered after the insertion point.</value> 
    /// <remarks> 
    /// <para>A value of Color.Empty indicates that the default background color is used.</para> 
    /// <para>If the selection contains more than one background 
    /// color, then this property will indicate it by 
    /// returning Color.Empty.</para> 
    /// </remarks> 
    [Browsable(false),
    DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color SelectionBackColor2
    {
        get
        {
            CHARFORMAT2 fmt = new CHARFORMAT2();
            fmt.cbSize = Marshal.SizeOf(fmt);

            // Get the background color 
            SendMessage(new HandleRef(this, Handle), EM_GETCHARFORMAT, SCF_SELECTION, ref fmt);

            // Default to Color.Empty as there could be 
            // several colors present in this selection 
            if ((fmt.dwMask & CFM_BACKCOLOR) == 0)
                return Color.Empty;

            // Default to Color.Empty if the background color is automatic 
            if ((fmt.dwEffects & CFE_AUTOBACKCOLOR) == CFE_AUTOBACKCOLOR)
                return Color.Empty;

            // Deal with the weird Windows color format 
            return ColorTranslator.FromWin32(fmt.crBackColor);
        }
        set
        {
            CHARFORMAT2 fmt = new CHARFORMAT2();
            fmt.cbSize = Marshal.SizeOf(fmt);
            fmt.dwMask = CFM_BACKCOLOR;
            if (value.IsEmpty)
                fmt.dwEffects = CFE_AUTOBACKCOLOR;
            else
                fmt.crBackColor = ColorTranslator.ToWin32(value);

            // Set the background color 
            SendMessage(new HandleRef(this, Handle), EM_SETCHARFORMAT, SCF_SELECTION, ref fmt);
        }
    }

    /// <summary>
    /// Gets or sets the text color of the current selection.
    /// </summary>
    [Browsable(false),
    DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color SelectionColor2
    {
        get
        {
            CHARFORMAT2 fmt = new CHARFORMAT2();
            fmt.cbSize = Marshal.SizeOf(fmt);

            // Get the background color 
            SendMessage(new HandleRef(this, Handle), EM_GETCHARFORMAT, SCF_SELECTION, ref fmt);

            // Default to Color.Empty as there could be 
            // several colors present in this selection 
            if ((fmt.dwMask & CFM_COLOR) == 0)
                return Color.Empty;

            // Default to Color.Empty if the background color is automatic 
            if ((fmt.dwEffects & CFE_AUTOCOLOR) == CFE_AUTOCOLOR)
                return Color.Empty;

            // Deal with the weird Windows color format 
            return ColorTranslator.FromWin32(fmt.crTextColor);
        }
        set
        {
            CHARFORMAT2 fmt = new CHARFORMAT2();
            fmt.cbSize = Marshal.SizeOf(fmt);
            fmt.dwMask = CFM_COLOR;
            if (value.IsEmpty)
                fmt.dwEffects = CFE_AUTOCOLOR;
            else
                fmt.crTextColor = ColorTranslator.ToWin32(value);

            // Set the background color 
            SendMessage(new HandleRef(this, Handle), EM_SETCHARFORMAT, SCF_SELECTION, ref fmt);
        }
    }

    #endregion
    #region Property: SelectionAlignment

    /// <summary> 
    /// Gets or sets the text alignment to apply to the current 
    /// selection or insertion point. 
    /// </summary> 
    /// <value>A member of the <see cref="RichTextAlign"/> enumeration that represents 
    /// the text alignment to apply to the current text selection or to text entered 
    /// after the insertion point.</value> 
    [Browsable(false),
    DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new RichTextAlign SelectionAlignment
    {
        get
        {
            PARAFORMAT2 fmt = new PARAFORMAT2();
            fmt.cbSize = Marshal.SizeOf(fmt);
            SendMessage(new HandleRef(this, Handle), EM_GETPARAFORMAT, SCF_SELECTION, ref fmt);

            if ((fmt.dwMask & PFM_ALIGNMENT) == 0)
                return RichTextAlign.Unknown;
            else
                return (RichTextAlign)fmt.wAlignment;
        }
        set
        {
            PARAFORMAT2 fmt = new PARAFORMAT2();
            fmt.cbSize = Marshal.SizeOf(fmt);
            fmt.dwMask = PFM_ALIGNMENT;
            fmt.wAlignment = (short)value;

            // Set the alignment 
            SendMessage(new HandleRef(this, Handle), EM_SETPARAFORMAT, SCF_SELECTION, ref fmt);
        }
    }

    #endregion

    // Convert the unit that is used by the .NET framework (1/100 inch)
    // and the unit that is used by Win32 API calls (twips 1/1440 inch)
    private const double AnInch = 14.4;

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CHARRANGE
    {
        public int cpMin; // First character of range (0 for start of doc)
        public int cpMax; // Last character of range (-1 for end of doc)
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct FORMATRANGE
    {
        public IntPtr hdc; // Actual DC to draw on
        public IntPtr hdcTarget; // Target DC for determining text formatting
        public RECT rc; // Region of the DC to draw to (in twips)
        public RECT rcPage; // Region of the whole DC (page size) (in twips)
        public CHARRANGE chrg; // Range of text to draw (see above declaration)
    }

    private const int WM_USER = 0x400;
    private const int EM_FORMATRANGE = WM_USER + 57;
    private const int BULLET_NUMBER = 2;
    


    #endregion

    #region CHAR STYLE

    [Browsable(false)]
    public class CharStyle
    {
        bool _bold = false, _italic = false,
             _strikeout = false, _underline = false,
             _link = false;

        public CharStyle()
        {
            _bold = false;
            _italic = false;
            _strikeout = false;
            _underline = false;
            _link = false;
        }

        public CharStyle(bool bold, bool italic, bool underline, bool strikeout, bool link)
        {
            _bold = bold;
            _italic = italic;
            _underline = underline;
            _strikeout = strikeout;
            _link = link;
        }


        /// <summary>
        /// Indicates whether font is bold
        /// </summary>
        public bool Bold
        {
            get { return _bold; }
            set { _bold = value; }
        }
        /// <summary>
        /// If true font is italic, otherwise false
        /// </summary>
        public bool Italic
        {
            get { return _italic; }
            set { _italic = value; }
        }
        /// <summary>
        /// If true selected text is underlined, otherwise false
        /// </summary>
        public bool Underline
        {
            get { return _underline; }
            set { _underline = value; }
        }
        /// <summary>
        /// If true, RichTextBox draw line through the baseline text
        /// </summary>
        public bool Strikeout
        {
            get { return _strikeout; }
            set { _strikeout = value; }
        }
        /// <summary>
        /// If true, then it is a hyperlink, otherwise - text
        /// </summary>
        public bool Link
        {
            get { return _link; }
            set { _link = value; }
        }
    }    

    private const uint DEFAULT_EFFECTS = 1140850688;
    private const uint DEFAULT_MASK = 4278190079;

    [Browsable(false)]
    public CharStyle SelectionCharStyle
    {
        get
        {
            CHARFORMAT2 param = new CHARFORMAT2();
            param.cbSize = Marshal.SizeOf(param);            
            SendMessage(new HandleRef(this, Handle), EM_GETCHARFORMAT, SCF_SELECTION, ref param);
            
            if (param.crBackColor > 0 && param.crTextColor > 0)
            { }
            else if (param.crBackColor > 0)
            {
                param.dwEffects -= 1073741824;
            }
            else if (param.crTextColor > 0)
            {
                param.dwEffects -= 67108864;
            }
                
            
            CharStyle cs = new CharStyle();
            //MessageBox.Show(param.dwEffects.ToString());
            //I know, it difficult, but all combinations must be processed
            if (param.dwEffects == DEFAULT_EFFECTS + CFE_BOLD || param.dwEffects == CFE_BOLD) cs.Bold = true;
            else if (param.dwEffects == DEFAULT_EFFECTS + CFE_ITALIC || param.dwEffects == CFE_ITALIC) cs.Italic = true;
            else if (param.dwEffects == DEFAULT_EFFECTS + CFE_UNDERLINE || param.dwEffects == CFE_UNDERLINE) cs.Underline = true;
            else if (param.dwEffects == DEFAULT_EFFECTS + CFE_STRIKEOUT || param.dwEffects == CFE_STRIKEOUT) cs.Strikeout = true;
            else if (param.dwEffects == DEFAULT_EFFECTS + CFE_LINK || param.dwEffects == CFE_LINK) cs.Link = true;
            else if (param.dwEffects == DEFAULT_EFFECTS + CFE_BOLD + CFE_ITALIC || param.dwEffects == CFE_BOLD + CFE_ITALIC) { cs.Bold = true; cs.Italic = true; }
            else if (param.dwEffects == DEFAULT_EFFECTS + CFE_BOLD + CFE_UNDERLINE || param.dwEffects == CFE_BOLD + CFE_UNDERLINE) { cs.Bold = true; cs.Underline = true; }
            else if (param.dwEffects == DEFAULT_EFFECTS + CFE_BOLD + CFE_STRIKEOUT || param.dwEffects == CFE_BOLD + CFE_STRIKEOUT) { cs.Bold = true; cs.Strikeout = true; }
            else if (param.dwEffects == DEFAULT_EFFECTS + CFE_BOLD + CFE_LINK || param.dwEffects == CFE_BOLD + CFE_LINK) { cs.Bold = true; cs.Link = true; }
            else if (param.dwEffects == DEFAULT_EFFECTS + CFE_ITALIC + CFE_UNDERLINE || param.dwEffects == CFE_ITALIC + CFE_UNDERLINE) { cs.Italic = true; cs.Underline = true; }
            else if (param.dwEffects == DEFAULT_EFFECTS + CFE_ITALIC + CFE_STRIKEOUT || param.dwEffects == CFE_ITALIC + CFE_STRIKEOUT) { cs.Italic = true; cs.Strikeout = true; }
            else if (param.dwEffects == DEFAULT_EFFECTS + CFE_ITALIC + CFE_LINK || param.dwEffects == CFE_ITALIC + CFE_LINK) { cs.Italic = true; cs.Link = true; }
            else if (param.dwEffects == DEFAULT_EFFECTS + CFE_UNDERLINE + CFE_STRIKEOUT || param.dwEffects == CFE_UNDERLINE + CFE_STRIKEOUT) { cs.Underline = true; cs.Strikeout = true; }
            else if (param.dwEffects == DEFAULT_EFFECTS + CFE_STRIKEOUT + CFE_LINK || param.dwEffects == CFE_STRIKEOUT + CFE_LINK) { cs.Strikeout = true; cs.Link = true; }
            else if (param.dwEffects == DEFAULT_EFFECTS + CFE_BOLD + CFE_ITALIC + CFE_UNDERLINE || param.dwEffects == CFE_BOLD + CFE_ITALIC + CFE_UNDERLINE) { cs.Bold = true; cs.Italic = true; cs.Underline = true; }
            else if (param.dwEffects == DEFAULT_EFFECTS + CFE_BOLD + CFE_ITALIC + CFE_STRIKEOUT || param.dwEffects == CFE_BOLD + CFE_ITALIC + CFE_STRIKEOUT) { cs.Bold = true; cs.Italic = true; cs.Strikeout = true; }
            else if (param.dwEffects == DEFAULT_EFFECTS + CFE_BOLD + CFE_ITALIC + CFE_LINK || param.dwEffects == CFE_BOLD + CFE_ITALIC + CFE_LINK) { cs.Bold = true; cs.Italic = true; cs.Link = true; }
            else if (param.dwEffects == DEFAULT_EFFECTS + CFE_BOLD + CFE_UNDERLINE + CFE_STRIKEOUT || param.dwEffects == CFE_BOLD + CFE_UNDERLINE + CFE_STRIKEOUT) { cs.Bold = true; cs.Underline = true; cs.Strikeout = true; }
            else if (param.dwEffects == DEFAULT_EFFECTS + CFE_BOLD + CFE_UNDERLINE + CFE_LINK || param.dwEffects == CFE_BOLD + CFE_UNDERLINE + CFE_LINK) { cs.Bold = true; cs.Underline = true; cs.Link = true; }
            else if (param.dwEffects == DEFAULT_EFFECTS + CFE_ITALIC + CFE_UNDERLINE + CFE_STRIKEOUT || param.dwEffects == CFE_ITALIC + CFE_UNDERLINE + CFE_STRIKEOUT) { cs.Italic = true; cs.Underline = true; cs.Strikeout = true; }
            else if (param.dwEffects == DEFAULT_EFFECTS + CFE_ITALIC + CFE_UNDERLINE + CFE_LINK || param.dwEffects == CFE_ITALIC + CFE_UNDERLINE + CFE_LINK) { cs.Italic = true; cs.Underline = true; cs.Link = true; }
            else if (param.dwEffects == DEFAULT_EFFECTS + CFE_ITALIC + CFE_STRIKEOUT + CFE_LINK || param.dwEffects == CFE_ITALIC + CFE_STRIKEOUT + CFE_LINK) { cs.Italic = true; cs.Strikeout = true; cs.Link = true; }
            else if (param.dwEffects == DEFAULT_EFFECTS + CFE_UNDERLINE + CFE_STRIKEOUT + CFE_LINK || param.dwEffects == CFE_UNDERLINE + CFE_STRIKEOUT + CFE_LINK) { cs.Underline = true; cs.Strikeout = true; cs.Link = true; }
            else if (param.dwEffects == DEFAULT_EFFECTS + CFE_BOLD + CFE_ITALIC + CFE_UNDERLINE + CFE_STRIKEOUT || param.dwEffects == CFE_BOLD + CFE_ITALIC + CFE_UNDERLINE + CFE_STRIKEOUT) { cs.Bold = true; cs.Italic = true; cs.Underline = true; cs.Strikeout = true; }
            else if (param.dwEffects == DEFAULT_EFFECTS + CFE_BOLD + CFE_ITALIC + CFE_UNDERLINE + CFE_LINK || param.dwEffects == CFE_BOLD + CFE_ITALIC + CFE_UNDERLINE + CFE_LINK) { cs.Bold = true; cs.Italic = true; cs.Underline = true; cs.Link = true; }
            else if (param.dwEffects == DEFAULT_EFFECTS + CFE_BOLD + CFE_ITALIC + CFE_STRIKEOUT + CFE_LINK || param.dwEffects == CFE_BOLD + CFE_ITALIC + CFE_STRIKEOUT + CFE_LINK) { cs.Bold = true; cs.Italic = true; cs.Strikeout = true; cs.Link = true; }
            else if (param.dwEffects == DEFAULT_EFFECTS + CFE_BOLD + CFE_UNDERLINE + CFE_STRIKEOUT + CFE_LINK || param.dwEffects == CFE_BOLD + CFE_UNDERLINE + CFE_STRIKEOUT + CFE_LINK) { cs.Bold = true; cs.Underline = true; cs.Strikeout = true; cs.Link = true; }
            else if (param.dwEffects == DEFAULT_EFFECTS + CFE_BOLD + CFE_UNDERLINE + CFE_STRIKEOUT + CFE_LINK || param.dwEffects == CFE_BOLD + CFE_UNDERLINE + CFE_STRIKEOUT + CFE_LINK) { cs.Bold = true; cs.Underline = true; cs.Strikeout = true; cs.Link = true; }
            else if (param.dwEffects == DEFAULT_EFFECTS + CFE_ITALIC + CFE_UNDERLINE + CFE_STRIKEOUT + CFE_LINK || param.dwEffects == CFE_ITALIC + CFE_UNDERLINE + CFE_STRIKEOUT + CFE_LINK) { cs.Italic = true; cs.Underline = true; cs.Strikeout = true; cs.Link = true; }
            else if (param.dwEffects == DEFAULT_EFFECTS + CFE_BOLD + CFE_ITALIC + CFE_UNDERLINE + CFE_STRIKEOUT + CFE_LINK || param.dwEffects == CFE_BOLD + CFE_ITALIC + CFE_UNDERLINE + CFE_STRIKEOUT + CFE_LINK) { cs.Bold = true; cs.Italic = true; cs.Underline = true; cs.Strikeout = true; cs.Link = true; }
            else cs = new CharStyle(); //style was not recognized...            
            return cs;
        }
        set
        {
            CHARFORMAT2 param = new CHARFORMAT2();
            param.cbSize = Marshal.SizeOf(param);
            SendMessage(new HandleRef(this, Handle), EM_GETCHARFORMAT, SCF_SELECTION, ref param);
            uint effects = 0, mask = 0;

            //we need to set both mask and style
            //otherwise we can loose some formatting            
            if (value.Bold == true) { effects += CFE_BOLD; mask += CFM_BOLD; } else { mask += CFM_BOLD; }
            if (value.Italic == true) { effects += CFE_ITALIC; mask += CFM_ITALIC; } else { mask += CFM_ITALIC; }
            if (value.Underline == true) { effects += CFE_UNDERLINE; mask += CFM_UNDERLINE; } else { mask += CFM_UNDERLINE; }
            if (value.Strikeout == true) { effects += CFE_STRIKEOUT; mask += CFM_STRIKEOUT; } else { mask += CFM_STRIKEOUT; }
            if (value.Link == true) { effects += CFE_LINK; mask += CFM_LINK; } else { mask += CFM_LINK; }
            
            param.dwEffects = effects;
            param.dwMask = mask;
            
            SendMessage(new HandleRef(this, Handle), EM_SETCHARFORMAT, SCF_SELECTION, ref param);
        }
    }

    string GetStringFromChar(char[] val)
    {
        if (val.Length == 0) return "";
        string s = "";

        for (int i = 0; i < val.Length; i++)
            s += val.GetValue(i).ToString();

        return s;
    }

    public Font SelectionFont2
    {
        get
        {
            CHARFORMAT2 param = new CHARFORMAT2();
            param.cbSize = Marshal.SizeOf(param);
            SendMessage(new HandleRef(this, Handle), EM_GETCHARFORMAT, SCF_SELECTION, ref param);

            Font f = new Font(GetStringFromChar(param.szFaceName), (float)(param.yHeight / 20), GraphicsUnit.Inch);
            Font f2 = new Font(f.Name, (float)f.Size);
            
            return f;
        }
        set
        {
            CHARFORMAT2 param = new CHARFORMAT2();
            param.cbSize = Marshal.SizeOf(param);
            SendMessage(new HandleRef(this, Handle), EM_GETCHARFORMAT, SCF_SELECTION, ref param);
            param.dwMask = CFM_FACE + CFM_SIZE + CFM_CHARSET;
            param.bCharSet = value.GdiCharSet;
            char[] name = new char[64];
            name = value.Name.ToCharArray();
            param.szFaceName = new char[64];
            name.CopyTo(param.szFaceName, 0);
            param.yHeight = (int)(value.Size * AnInch);
            SendMessage(new HandleRef(this, Handle), EM_SETCHARFORMAT, SCF_SELECTION, ref param);
        }
    }

    #endregion

    #region LINE SPACING

    public class ParaLineSpacing
    {
        public enum LineSpacingStyle : byte
        {
            /// <summary>
            /// Single spacing. The ExactSpacing member is ignored.
            /// </summary>
            Single = 0,
            /// <summary>
            /// One-and-a-half spacing. The ExactSpacing member is ignored.
            /// </summary>
            OneAndAHalf = 1,
            /// <summary>
            /// Double spacing. The ExactSpacing member is ignored.
            /// </summary>
            Double = 2,
            /// <summary>
            /// The ExactSpacing member specifies the spacingfrom one line to the next, in twips. 
            /// However, if ExactSpacing specifies a value that is less than single spacing, 
            /// the control displays single-spaced text.
            /// </summary>
            ExactFixed = 3,
            /// <summary>
            /// The ExactSpacing member specifies the spacing from one line to the next, in twips. 
            /// The control uses the exact spacing specified, even if ExactSpacing specifies a 
            /// value that is less than single spacing.
            /// </summary>
            ExactFree = 4,
            /// <summary>
            /// The value of dyLineSpacing / 20 is the spacing, in lines, from one line to the next. 
            /// Thus, setting dyLineSpacing to 20 produces single-spaced text, 40 is double spaced, 
            /// 60 is triple spaced, and so on.
            /// </summary>
            Relative = 5,
            /// <summary>
            /// Value was not recognized. Do not send this value through SendMessage, RichTextBox will
            /// not recognize it. Added for internal purposes.
            /// </summary>
            Unknown = 6
        }

        LineSpacingStyle style = LineSpacingStyle.Single; private int spacing = 20;

        public LineSpacingStyle SpacingStyle
        {
            get { return style; }
            set { style = value; }
        }
        public int ExactSpacing
        {
            get { return spacing; }
            set { spacing = value; }
        }
    }

    public ParaLineSpacing SelectionLineSpacing
    {
        get 
        {
            PARAFORMAT2 param = new PARAFORMAT2();
            param.cbSize = Marshal.SizeOf(param);
            SendMessage(new HandleRef(this, Handle), EM_GETPARAFORMAT, SCF_SELECTION, ref param);

            ParaLineSpacing pls = new ParaLineSpacing();

            if (param.bLineSpacingRule >= 1 && param.bLineSpacingRule <= 5)
                pls.SpacingStyle = (ParaLineSpacing.LineSpacingStyle)param.bLineSpacingRule;
            else
                pls.SpacingStyle = ParaLineSpacing.LineSpacingStyle.Unknown;

            pls.ExactSpacing = param.dyLineSpacing;
            
            return pls;
        }
        set 
        {
            PARAFORMAT2 param = new PARAFORMAT2();
            param.cbSize = Marshal.SizeOf(param);
            SendMessage(new HandleRef(this, Handle), EM_GETPARAFORMAT, SCF_SELECTION, ref param);

            param.dyLineSpacing = value.ExactSpacing;
            if (value.SpacingStyle != ParaLineSpacing.LineSpacingStyle.Unknown)
                param.bLineSpacingRule = (byte)value.SpacingStyle;
            else
                param.bLineSpacingRule = 1;
        }
    }

    /// <summary>
    /// Space, in twips, before line
    /// </summary>
    public int SelectionSpaceBefore
    {
        get
        {
            PARAFORMAT2 param = new PARAFORMAT2();
            param.cbSize = Marshal.SizeOf(param);
            SendMessage(new HandleRef(this, Handle), EM_GETPARAFORMAT, SCF_SELECTION, ref param);

            return param.dySpaceBefore;
        }
        set
        {
            PARAFORMAT2 param = new PARAFORMAT2();
            param.cbSize = Marshal.SizeOf(param);
            SendMessage(new HandleRef(this, Handle), EM_GETPARAFORMAT, SCF_SELECTION, ref param);

            param.dwMask = PFM_SPACEBEFORE;
            param.dySpaceBefore = value;

            SendMessage(new HandleRef(this, Handle), EM_SETPARAFORMAT, SCF_SELECTION, ref param);
        }
    }
    /// <summary>
    /// Space, in twips, after line
    /// </summary>
    public int SelectionSpaceAfter
    {
        get
        {
            PARAFORMAT2 param = new PARAFORMAT2();
            param.cbSize = Marshal.SizeOf(param);
            SendMessage(new HandleRef(this, Handle), EM_GETPARAFORMAT, SCF_SELECTION, ref param);

            return param.dySpaceAfter;
        }
        set
        {
            PARAFORMAT2 param = new PARAFORMAT2();
            param.cbSize = Marshal.SizeOf(param);
            SendMessage(new HandleRef(this, Handle), EM_GETPARAFORMAT, SCF_SELECTION, ref param);

            param.dwMask = PFM_SPACEAFTER;
            param.dySpaceAfter = value;

            SendMessage(new HandleRef(this, Handle), EM_SETPARAFORMAT, SCF_SELECTION, ref param);
        }
    }

    #endregion

    #region CHAR OFFSET

    public enum OffsetType
    {
        Subscript, Superscript, None
    }

    public OffsetType SelectionOffsetType
    {
        get 
        {
            CHARFORMAT2 param = new CHARFORMAT2();
            param.cbSize = Marshal.SizeOf(param);
            SendMessage(new HandleRef(this, Handle), EM_GETCHARFORMAT, SCF_SELECTION, ref param);

            if (param.yOffset < 0)
                return OffsetType.Superscript;
            else if (param.yOffset > 0)
                return OffsetType.Subscript;
            else
                return OffsetType.None;
        }
        set 
        {
            CHARFORMAT2 param = new CHARFORMAT2();
            param.cbSize = Marshal.SizeOf(param);
            SendMessage(new HandleRef(this, Handle), EM_GETCHARFORMAT, SCF_SELECTION, ref param);

            int _offset = 0;

            switch (value)
            {
                case OffsetType.Subscript:
                    if (param.yOffset < 0) //super
                        _offset = Math.Abs(param.yOffset);
                    else if (param.yOffset > 0) //sub
                        _offset = param.yOffset;
                    else //none
                        _offset = 0;
                    break;
                case OffsetType.Superscript:
                    if (param.yOffset < 0) //super
                        _offset = param.yOffset;
                    else if (param.yOffset > 0) //sub
                        _offset = -param.yOffset;
                    else //none
                        _offset = 0;
                    break;
                case OffsetType.None:
                    _offset = 0;
                    break;
                default:
                    break;
            }

            param.dwMask = CFM_OFFSET;
            param.yOffset = _offset;

            SendMessage(new HandleRef(this, Handle), EM_GETCHARFORMAT, SCF_SELECTION, ref param);
        }
    }

    #endregion

    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    // :-(  BORDER STYLE IS NOT SUPPORTED BY RichTextBox 2.0 :-(
    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    #region BORDERS

    [Browsable(false)]
    public class ParaBorders
    {
        private bool _bottom = false, _top = false, _left = false, 
             _right = false, _inside = false, _outside = false,
             _autocolor = false;
        private short _bWidth = 1, _bOffset = 1;
        private ParaBorderStyle _style = ParaBorderStyle.None;
        private ParaBorderColor _color = ParaBorderColor.Black;

        public ParaBorders()
        {
            _bottom = false;
            _top = false;
            _left = false;
            _right = false;
        }
        public ParaBorders(bool bottom, bool top, bool left, bool right)
        {
            _bottom = bottom;
            _top = top;
            _left = left;
            _right = right;
        }

        public bool Bottom
        {
            get { return _bottom; }
            set { _bottom = value; }
        }
        public bool Top
        {
            get { return _top; }
            set { _top = value; }
        }
        public bool Left
        {
            get { return _left; }
            set { _left = value; }
        }
        public bool Right
        {
            get { return _right; }
            set { _right = value; }
        }
        public bool Inside
        {
            get { return _inside; }
            set { _inside = value; }
        }
        public bool Outside
        {
            get { return _outside; }
            set { _outside = value; }
        }

        public bool UseAutoColor
        {
            get { return _autocolor; }
            set { _autocolor = value; }
        }

        public short BorderWidth
        {
            get { return _bWidth; }
            set { _bWidth = value; }
        }
        public short BorderOffset
        {
            get { return _bOffset; }
            set { _bOffset = value; }
        }

        public ParaBorderStyle BorderStyle
        {
            get { return _style; }
            set { _style = value; }
        }
        public ParaBorderColor BorderColor
        {
            get { return _color; }
            set { _color = value; }
        }

        /// <summary>
        /// Represents styles for borders
        /// </summary>
        public enum ParaBorderStyle
        {
            None, 
            Point3_4, 
            Point11_2, 
            Point21_4, 
            Point3, 
            Point41_2,
            Point6, 
            PointDouble3_4, 
            PointDouble11_2, 
            PointDouble21_4, 
            PointGray3_4, 
            PointGrayDashed3_4            
        }
        public enum ParaBorderColor
        {
            Black, 
            Blue, 
            Cyan, 
            Green, 
            Magenta, 
            Red, 
            Yellow,
            White, 
            DarkBlue, 
            DarkCyan, 
            DarkGreen, 
            DarkMagenta,
            DarkRed, 
            DarkYellow, 
            DarkGray, 
            LightGray
        }

        public short GetData()
        {
            //try
            //{
            BitArray ba = new BitArray(16);
            //Set all bits to 0
            ba.SetAll(false);

            /*
            Left border     00000001
            Right border    00000010
            Top border      00000100
            Bottom border   00001000
            Inside borders  00010000
            Outside borders 00100000
            Autocolor       01000000  If this bit is set, the color index in bits 12 to 15 is not used.
             */
            
            ba.Set(0, true); //this is always 0

            #region WHICH BORDERS
            if (_autocolor == true) ba.Set(1, true); 
            if (_outside == true) ba.Set(2, true);   
            if (_inside == true) ba.Set(3, true);
            if (_bottom == true) ba.Set(4, true);
            if (_top == true) ba.Set(5, true);
            if (_right == true) ba.Set(6, true);
            if (_left == true) ba.Set(7, true);
            #endregion

            #region BORDER STYLE
            /*
            0000   None
            0001   3/4 point
            0010   11/2 point
            0011   21/4 point
            0100   3 point
            0101   41/2 point
            0110   6 point
            0111   3/4 point double
            1000   11/2 point double
            1001   21/4 point double
            1010   3/4 point gray
            1011   3/4 point gray dashed
            */

            switch (_style)
            {
                case ParaBorderStyle.None:
                    ba.Set(8, false); ba.Set(9, false); ba.Set(10, false); ba.Set(11, false);
                    break;
                case ParaBorderStyle.Point3_4:
                    ba.Set(8, false); ba.Set(9, false); ba.Set(10, false); ba.Set(11, true);
                    break;
                case ParaBorderStyle.Point11_2:
                    ba.Set(8, false); ba.Set(9, false); ba.Set(10, true); ba.Set(11, false);
                    break;
                case ParaBorderStyle.Point21_4:
                    ba.Set(8, false); ba.Set(9, false); ba.Set(10, true); ba.Set(11, true);
                    break;
                case ParaBorderStyle.Point3:
                    ba.Set(8, false); ba.Set(9, true); ba.Set(10, false); ba.Set(11, false);
                    break;
                case ParaBorderStyle.Point41_2:
                    ba.Set(8, false); ba.Set(9, true); ba.Set(10, false); ba.Set(11, true);
                    break;
                case ParaBorderStyle.Point6:
                    ba.Set(8, false); ba.Set(9, true); ba.Set(10, true); ba.Set(11, false);
                    break;
                case ParaBorderStyle.PointDouble3_4:
                    ba.Set(8, false); ba.Set(9, true); ba.Set(10, true); ba.Set(11, true);
                    break;
                case ParaBorderStyle.PointDouble11_2:
                    ba.Set(8, true); ba.Set(9, false); ba.Set(10, false); ba.Set(11, false);
                    break;
                case ParaBorderStyle.PointDouble21_4:
                    ba.Set(8, true); ba.Set(9, false); ba.Set(10, false); ba.Set(11, true);
                    break;
                case ParaBorderStyle.PointGray3_4:
                    ba.Set(8, true); ba.Set(9, false); ba.Set(10, true); ba.Set(11, false);
                    break;
                case ParaBorderStyle.PointGrayDashed3_4:
                    ba.Set(8, true); ba.Set(9, false); ba.Set(10, true); ba.Set(11, true);
                    break;
                default: // no borders
                    ba.Set(8, false); ba.Set(9, false); ba.Set(10, false); ba.Set(11, false); 
                    break;
            }
            #endregion

            #region BORDER COLOR

            /*
            0000    Black
            0001    Blue
            0010    Cyan
            0011    Green
            0100    Magenta
            0101    Red
            0110    Yellow
            0111    White
            1000    DarkBlue
            1001    DarkCyan
            1010    DarkGreen
            1011    DarkMagenta
            1100    DarkRed
            1101    DarkYellow
            1110    DarkGray
            1111    LightGray
            */

            switch (_color)
            {
                case ParaBorderColor.Black:
                    ba.Set(12, false); ba.Set(13, false); ba.Set(14, false); ba.Set(15, false);
                    break;
                case ParaBorderColor.Blue:
                    ba.Set(12, false); ba.Set(13, false); ba.Set(14, false); ba.Set(15, true);
                    break;
                case ParaBorderColor.Cyan:
                    ba.Set(12, false); ba.Set(13, false); ba.Set(14, true); ba.Set(15, false);
                    break;
                case ParaBorderColor.Green:
                    ba.Set(12, false); ba.Set(13, false); ba.Set(14, true); ba.Set(15, true);
                    break;
                case ParaBorderColor.Magenta:
                    ba.Set(12, false); ba.Set(13, true); ba.Set(14, false); ba.Set(15, false);
                    break;
                case ParaBorderColor.Red:
                    ba.Set(12, false); ba.Set(13, true); ba.Set(14, false); ba.Set(15, true);
                    break;
                case ParaBorderColor.Yellow:
                    ba.Set(12, false); ba.Set(13, true); ba.Set(14, true); ba.Set(15, false);
                    break;
                case ParaBorderColor.White:
                    ba.Set(12, false); ba.Set(13, true); ba.Set(14, true); ba.Set(15, true);
                    break;
                case ParaBorderColor.DarkBlue:
                    ba.Set(12, true); ba.Set(13, false); ba.Set(14, false); ba.Set(15, false);
                    break;
                case ParaBorderColor.DarkCyan:
                    ba.Set(12, true); ba.Set(13, false); ba.Set(14, false); ba.Set(15, true);
                    break;
                case ParaBorderColor.DarkGreen:
                    ba.Set(12, true); ba.Set(13, false); ba.Set(14, true); ba.Set(15, false);
                    break;
                case ParaBorderColor.DarkMagenta:
                    ba.Set(12, true); ba.Set(13, false); ba.Set(14, true); ba.Set(15, true);
                    break;
                case ParaBorderColor.DarkRed:
                    ba.Set(12, true); ba.Set(13, true); ba.Set(14, false); ba.Set(15, false);
                    break;
                case ParaBorderColor.DarkYellow:
                    ba.Set(12, true); ba.Set(13, true); ba.Set(14, false); ba.Set(15, true);
                    break;
                case ParaBorderColor.DarkGray:
                    ba.Set(12, true); ba.Set(13, true); ba.Set(14, true); ba.Set(15, false);
                    break;
                case ParaBorderColor.LightGray:
                    ba.Set(12, true); ba.Set(13, true); ba.Set(14, true); ba.Set(15, true);
                    break;
                default:
                    ba.Set(12, false); ba.Set(13, false); ba.Set(14, false); ba.Set(15, false); //Default to Black
                    break;
            }

            #endregion

            byte[] arr = (byte[])Array.CreateInstance(typeof(byte), 2);
            
            ba.CopyTo(arr, 0);
            
            return BitConverter.ToInt16(arr, 0);            
            //}
            //catch (Exception)
            //{
            //    return 0;
            //}
        }
    }
    /*
    public ParaBorders SelectionBorders
    {
        /*get 
        {
            PARAFORMAT2 param = new PARAFORMAT2();
            param.cbSize = Marshal.SizeOf(param);
            SendMessage(new HandleRef(this, Handle), EM_GETPARAFORMAT, SCF_SELECTION, ref param);

            ParaBorders pb = new ParaBorders();

            pb.BorderWidth = param.wBorderWidth;
            pb.BorderOffset = param.wBorderSpace;

            //MessageBox.Show(Convert.ToString(param.wBorders));

            return pb;

        }

        set
        {
            PARAFORMAT2 param = new PARAFORMAT2();
            param.cbSize = Marshal.SizeOf(param);
            SendMessage(new HandleRef(this, Handle), EM_GETPARAFORMAT, SCF_SELECTION, ref param);

            param.dwMask = PFM_BORDER;
            param.wBorders = value.GetData();
            param.wBorderWidth = value.BorderWidth;
            param.wBorderSpace = value.BorderOffset;

            SendMessage(new HandleRef(this, Handle), EM_SETPARAFORMAT, SCF_SELECTION, ref param);
        }
    }
*/
    #endregion

    #region LIST

    /// <summary>
    /// Represents style for lists
    /// </summary>
    public class ParaListStyle
    {
        public enum ListType : short
        {
            /// <summary>
            /// No list
            /// </summary>
            None = 0,
            /// <summary>
            /// Small letters (a, b, c ...)
            /// </summary>
            SmallLetters = 3,
            /// <summary>
            /// Capital letters (A, B, C ...)
            /// </summary>
            CapitalLetters = 4,
            /// <summary>
            /// Small roman (i, ii, iii ...)
            /// </summary>
            SmallRoman = 5,
            /// <summary>
            /// Capital roman (I, II, III ...)
            /// </summary>
            CapitalRoman = 6,
            /// <summary>
            /// Bulleted list (standard bullet)
            /// </summary>
            Bullet = 1,
            /// <summary>
            /// Arabic numbers
            /// </summary>
            Numbers = 2,
            /// <summary>
            /// User specifed bullets
            /// </summary>
            CharBullet = 7
        }
        public enum ListStyle : short
        {
            /// <summary>
            /// Follows the number with a right parenthesis.
            /// </summary>
            NumberAndParenthesis = 0,
            /// <summary>
            /// Encloses the number in parentheses.
            /// </summary>
            NumberInPar = 0x100,
            /// <summary>
            /// Follows the number with a period.
            /// </summary>
            NumberAndPeriod = 0x200,
            /// <summary>
            /// Displays only the number.
            /// </summary>
            OnlyNumber = 0x300,
            /// <summary>
            /// Continues a numbered list without applying the next number or bullet.
            /// </summary>
            ContinueWithNoNumber = 0x400
        }

        private short NUMBERING_START = 1;
        private short CHAR_BULLET_CODE_UNICODE = 0;
        ListStyle ls = ListStyle.NumberAndParenthesis;
        ListType lt = ListType.None;

        public short NumberingStart
        {
            get { return NUMBERING_START; }
            set { NUMBERING_START = value; }
        }
        public short BulletCharCode
        {
            get { return CHAR_BULLET_CODE_UNICODE; }
            set { CHAR_BULLET_CODE_UNICODE = value; }
        }
        public ListStyle Style
        {
            get { return ls; }
            set { ls = value; }
        }
        public ListType Type
        {
            get { return lt; }
            set { lt = value; }
        }
    }

    /// <summary>
    /// Gets or sets current selection list type if exists. Before applying sequence, 
    /// set NumberingStart property to the value from which you want to start list 
    /// numbering. (Default is 1). Before using bulleted list, set BulletCharCode
    /// property to the value of corresponding Unicode character, which will represent
    /// a bullet.
    /// </summary>
    public ParaListStyle SelectionListType
    {
        get
        {
            ParaListStyle retVal = new ParaListStyle();
            PARAFORMAT2 param = new PARAFORMAT2();
            param.cbSize = Marshal.SizeOf(param);
            SendMessage(new HandleRef(this, Handle), EM_GETPARAFORMAT, SCF_SELECTION, ref param);
            switch (param.wNumbering)
            {
                case 0:
                    retVal.Type = ParaListStyle.ListType.None;
                    break;
                case 3:
                    retVal.Type = ParaListStyle.ListType.SmallLetters;
                    break;
                case 4:
                    retVal.Type = ParaListStyle.ListType.CapitalLetters;
                    break;
                case 5:
                    retVal.Type = ParaListStyle.ListType.SmallRoman;
                    break;
                case 6:
                    retVal.Type = ParaListStyle.ListType.CapitalRoman;
                    break;
                case 2:
                    retVal.Type = ParaListStyle.ListType.Numbers;
                    break;
                case 1:
                    retVal.Type = ParaListStyle.ListType.Bullet;
                    break;
                default:
                    retVal.Type = ParaListStyle.ListType.CharBullet;
                    break;
            }
            switch (param.wNumberingStyle)
            {
                case 0:
                    retVal.Style = ParaListStyle.ListStyle.NumberAndParenthesis;
                    break;
                case 0x100:
                    retVal.Style = ParaListStyle.ListStyle.NumberInPar;
                    break;
                case 0x200:
                    retVal.Style = ParaListStyle.ListStyle.NumberAndPeriod;
                    break;
                case 0x300:
                    retVal.Style = ParaListStyle.ListStyle.OnlyNumber;
                    break;
                case 0x400:
                    retVal.Style = ParaListStyle.ListStyle.ContinueWithNoNumber;
                    break;
                default:
                    retVal.Style = ParaListStyle.ListStyle.NumberAndParenthesis;
                    break;
            }
            retVal.NumberingStart = param.wNumberingStart;

            return retVal;
        }
        set
        {
            PARAFORMAT2 param = new PARAFORMAT2();
            param.cbSize = Marshal.SizeOf(param);
            SendMessage(new HandleRef(this, Handle), EM_GETPARAFORMAT, SCF_SELECTION, ref param);

            param.dwMask = PFM_NUMBERING + PFM_NUMBERINGSTART + PFM_NUMBERINGSTYLE;
            param.wNumberingStart = (short)value.NumberingStart;
            param.wNumberingStyle = (short)value.Style;
            param.wNumbering = (short)value.Type;

            SendMessage(new HandleRef(this, Handle), EM_SETPARAFORMAT, SCF_SELECTION, ref param);
        }
    }

    #endregion

    #region PRINTING

    // Render the contents of the RichTextBox for printing
    //	Return the last character printed + 1 (printing start from this point for next page)
    public int Print(int charFrom, int charTo, PrintPageEventArgs e)
    {
        try
        {
            // Mark starting and ending character
            CHARRANGE cRange;
            cRange.cpMin = charFrom;
            cRange.cpMax = charTo;

            // Calculate the area to render and print
            RECT rectToPrint;
            rectToPrint.Top = (int)(e.MarginBounds.Top * AnInch);
            rectToPrint.Bottom = (int)(e.MarginBounds.Bottom * AnInch);
            rectToPrint.Left = (int)(e.MarginBounds.Left * AnInch);
            rectToPrint.Right = (int)(e.MarginBounds.Right * AnInch);

            // Calculate the size of the page
            RECT rectPage;
            rectPage.Top = (int)(e.PageBounds.Top * AnInch);
            rectPage.Bottom = (int)(e.PageBounds.Bottom * AnInch);
            rectPage.Left = (int)(e.PageBounds.Left * AnInch);
            rectPage.Right = (int)(e.PageBounds.Right * AnInch);

            IntPtr hdc = e.Graphics.GetHdc();

            FORMATRANGE fmtRange;
            fmtRange.chrg = cRange; // Indicate character from to character to
            fmtRange.hdc = hdc; // Use the same DC for measuring and rendering
            fmtRange.hdcTarget = hdc; // Point at printer hDC
            fmtRange.rc = rectToPrint; // Indicate the area on page to print
            fmtRange.rcPage = rectPage; // Indicate whole size of page

            IntPtr res = IntPtr.Zero;

            IntPtr wparam = IntPtr.Zero;
            wparam = new IntPtr(1);

            // Move the pointer to the FORMATRANGE structure in memory
            IntPtr lparam = IntPtr.Zero;
            lparam = Marshal.AllocCoTaskMem(Marshal.SizeOf(fmtRange));
            Marshal.StructureToPtr(fmtRange, lparam, false);

            // Send the rendered data for printing
            res = SendMessage(Handle, EM_FORMATRANGE, wparam, lparam);

            // Free the block of memory allocated
            Marshal.FreeCoTaskMem(lparam);

            // Release the device context handle obtained by a previous call
            e.Graphics.ReleaseHdc(hdc);

            // Return last + 1 character printer
            return res.ToInt32();
        }
        catch (Exception)
        {
            return -1;
        }
    }

    #endregion

    #endregion
    
    #region OLE PROCESSING

    #region "OLE definitions"

    private const short EM_SETOLECALLBACK = WM_USER + 70;

    //public delegate void OleCallback();
    //public event OleCallback OnOleEvent;

    // STGM
    [Flags(), ComVisible(false)]
    public enum STGM : int
    {
        STGM_DIRECT = 0x0,
        STGM_TRANSACTED = 0x10000,
        STGM_SIMPLE = 0x8000000,
        STGM_READ = 0x0,
        STGM_WRITE = 0x1,
        STGM_READWRITE = 0x2,
        STGM_SHARE_DENY_NONE = 0x40,
        STGM_SHARE_DENY_READ = 0x30,
        STGM_SHARE_DENY_WRITE = 0x20,
        STGM_SHARE_EXCLUSIVE = 0x10,
        STGM_PRIORITY = 0x40000,
        STGM_DELETEONRELEASE = 0x4000000,
        STGM_NOSCRATCH = 0x100000,
        STGM_CREATE = 0x1000,
        STGM_CONVERT = 0x20000,
        STGM_FAILIFTHERE = 0x0,
        STGM_NOSNAPSHOT = 0x200000,
    }

    // DVASPECT
    [Flags(), ComVisible(false)]
    public enum DVASPECT : int
    {
        DVASPECT_CONTENT = 1,
        DVASPECT_THUMBNAIL = 2,
        DVASPECT_ICON = 4,
        DVASPECT_DOCPRINT = 8,
        DVASPECT_OPAQUE = 16,
        DVASPECT_TRANSPARENT = 32,
    }

    // CLIPFORMAT
    [ComVisible(false)]
    public enum CLIPFORMAT : int
    {
        CF_TEXT = 1,
        CF_BITMAP = 2,
        CF_METAFILEPICT = 3,
        CF_SYLK = 4,
        CF_DIF = 5,
        CF_TIFF = 6,
        CF_OEMTEXT = 7,
        CF_DIB = 8,
        CF_PALETTE = 9,
        CF_PENDATA = 10,
        CF_RIFF = 11,
        CF_WAVE = 12,
        CF_UNICODETEXT = 13,
        CF_ENHMETAFILE = 14,
        CF_HDROP = 15,
        CF_LOCALE = 16,
        CF_MAX = 17,
        CF_OWNERDISPLAY = 0x80,
        CF_DSPTEXT = 0x81,
        CF_DSPBITMAP = 0x82,
        CF_DSPMETAFILEPICT = 0x83,
        CF_DSPENHMETAFILE = 0x8E,
    }

    // Object flags
    [Flags(), ComVisible(false)]
    public enum REOOBJECTFLAGS : uint
    {
        REO_NULL = 0x00000000,	            // No flags
        REO_READWRITEMASK = 0x0000003F,	    // Mask out RO bits
        REO_DONTNEEDPALETTE = 0x00000020,	// Object doesn't need palette
        REO_BLANK = 0x00000010,	            // Object is blank
        REO_DYNAMICSIZE = 0x00000008,	    // Object defines size always
        REO_INVERTEDSELECT = 0x00000004,	// Object drawn all inverted if sel
        REO_BELOWBASELINE = 0x00000002,	    // Object sits below the baseline
        REO_RESIZABLE = 0x00000001,	        // Object may be resized
        REO_LINK = 0x80000000,	            // Object is a link (RO)
        REO_STATIC = 0x40000000,	        // Object is static (RO)
        REO_SELECTED = 0x08000000,	        // Object selected (RO)
        REO_OPEN = 0x04000000,	            // Object open in its server (RO)
        REO_INPLACEACTIVE = 0x02000000,	    // Object in place active (RO)
        REO_HILITED = 0x01000000,	        // Object is to be hilited (RO)
        REO_LINKAVAILABLE = 0x00800000,	    // Link believed available (RO)
        REO_GETMETAFILE = 0x00400000	    // Object requires metafile (RO)
    }

    // OLERENDER
    [ComVisible(false)]
    public enum OLERENDER : int
    {
        OLERENDER_NONE = 0,
        OLERENDER_DRAW = 1,
        OLERENDER_FORMAT = 2,
        OLERENDER_ASIS = 3,
    }

    // TYMED
    [Flags(), ComVisible(false)]
    public enum TYMED : int
    {
        TYMED_NULL = 0,
        TYMED_HGLOBAL = 1,
        TYMED_FILE = 2,
        TYMED_ISTREAM = 4,
        TYMED_ISTORAGE = 8,
        TYMED_GDI = 16,
        TYMED_MFPICT = 32,
        TYMED_ENHMF = 64,
    }

    [StructLayout(LayoutKind.Sequential), ComVisible(false)]
    public struct FORMATETC
    {
        public CLIPFORMAT cfFormat;
        public IntPtr ptd;
        public DVASPECT dwAspect;
        public int lindex;
        public TYMED tymed;
    }

    [StructLayout(LayoutKind.Sequential), ComVisible(false)]
    public struct STGMEDIUM
    {
        //[MarshalAs(UnmanagedType.I4)]
        public int tymed;
        public IntPtr unionmember;
        public IntPtr pUnkForRelease;
    }

    [ComVisible(true),
    ComImport(),
    Guid("00000103-0000-0000-C000-000000000046"),
    InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IEnumFORMATETC
    {
        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int Next(
            [In, MarshalAs(UnmanagedType.U4)]
			int celt,
            [Out]
			FORMATETC rgelt,
            [In, Out, MarshalAs(UnmanagedType.LPArray)]
			int[] pceltFetched);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int Skip(
            [In, MarshalAs(UnmanagedType.U4)]
			int celt);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int Reset();

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int Clone(
            [Out, MarshalAs(UnmanagedType.LPArray)]
			IEnumFORMATETC[] ppenum);
    }

    [ComVisible(true), StructLayout(LayoutKind.Sequential)]
    public class COMRECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;

        public COMRECT()
        {
        }

        public COMRECT(int left, int top, int right, int bottom)
        {
            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;
        }

        public static COMRECT FromXYWH(int x, int y, int width, int height)
        {
            return new COMRECT(x, y, x + width, y + height);
        }
    }

    public enum GETOBJECTOPTIONS
    {
        REO_GETOBJ_NO_INTERFACES = 0x00000000,
        REO_GETOBJ_POLEOBJ = 0x00000001,
        REO_GETOBJ_PSTG = 0x00000002,
        REO_GETOBJ_POLESITE = 0x00000004,
        REO_GETOBJ_ALL_INTERFACES = 0x00000007,
    }

    public enum GETCLIPBOARDDATAFLAGS
    {
        RECO_PASTE = 0,
        RECO_DROP = 1,
        RECO_COPY = 2,
        RECO_CUT = 3,
        RECO_DRAG = 4
    }

    [StructLayout(LayoutKind.Sequential)]
    public class REOBJECT
    {
        public int cbStruct = Marshal.SizeOf(typeof(REOBJECT));	// Size of structure
        public int cp;											// Character position of object
        public Guid clsid;										// Class ID of object
        public IntPtr poleobj;								    // OLE object interface
        public IStorage pstg;									// Associated storage interface
        public IOleClientSite polesite;							// Associated client site interface
        public Size sizel;										// Size of object (may be 0,0)
        public uint dvAspect;									// Display aspect to use
        public uint dwFlags;									// Object status flags
        public uint dwUser;										// Dword for user's use
    }

    [ComVisible(true), Guid("0000010F-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAdviseSink
    {

        //C#r: UNDONE (Field in interface) public static readonly    Guid iid;
        void OnDataChange(
            [In]
			FORMATETC pFormatetc,
            [In]
			STGMEDIUM pStgmed);

        void OnViewChange(
            [In, MarshalAs(UnmanagedType.U4)]
			int dwAspect,
            [In, MarshalAs(UnmanagedType.I4)]
			int lindex);

        void OnRename(
            [In, MarshalAs(UnmanagedType.Interface)]
			object pmk);

        void OnSave();


        void OnClose();
    }

    [ComVisible(false), StructLayout(LayoutKind.Sequential)]
    public sealed class STATDATA
    {

        [MarshalAs(UnmanagedType.U4)]
        public int advf;
        [MarshalAs(UnmanagedType.U4)]
        public int dwConnection;

    }

    [ComVisible(false), StructLayout(LayoutKind.Sequential)]
    public sealed class tagOLEVERB
    {
        [MarshalAs(UnmanagedType.I4)]
        public int lVerb;

        [MarshalAs(UnmanagedType.LPWStr)]
        public String lpszVerbName;

        [MarshalAs(UnmanagedType.U4)]
        public int fuFlags;

        [MarshalAs(UnmanagedType.U4)]
        public int grfAttribs;

    }

    [ComVisible(true), ComImport(), Guid("00000104-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IEnumOLEVERB
    {

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int Next(
            [MarshalAs(UnmanagedType.U4)]
			int celt,
            [Out]
			tagOLEVERB rgelt,
            [Out, MarshalAs(UnmanagedType.LPArray)]
			int[] pceltFetched);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int Skip(
            [In, MarshalAs(UnmanagedType.U4)]
			int celt);

        void Reset();


        void Clone(
            out IEnumOLEVERB ppenum);


    }

    [ComVisible(true), Guid("00000105-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IEnumSTATDATA
    {

        //C#r: UNDONE (Field in interface) public static readonly    Guid iid;

        void Next(
            [In, MarshalAs(UnmanagedType.U4)]
			int celt,
            [Out]
			STATDATA rgelt,
            [Out, MarshalAs(UnmanagedType.LPArray)]
			int[] pceltFetched);


        void Skip(
            [In, MarshalAs(UnmanagedType.U4)]
			int celt);


        void Reset();


        void Clone(
            [Out, MarshalAs(UnmanagedType.LPArray)]
			IEnumSTATDATA[] ppenum);


    }

    [ComVisible(true), Guid("0000011B-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IOleContainer
    {


        void ParseDisplayName(
            [In, MarshalAs(UnmanagedType.Interface)] object pbc,
            [In, MarshalAs(UnmanagedType.BStr)]      string pszDisplayName,
            [Out, MarshalAs(UnmanagedType.LPArray)] int[] pchEaten,
            [Out, MarshalAs(UnmanagedType.LPArray)] object[] ppmkOut);


        void EnumObjects(
            [In, MarshalAs(UnmanagedType.U4)]        int grfFlags,
            [Out, MarshalAs(UnmanagedType.LPArray)] object[] ppenum);


        void LockContainer(
            [In, MarshalAs(UnmanagedType.I4)] int fLock);
    }

    [ComVisible(true),
    ComImport(),
    Guid("0000010E-0000-0000-C000-000000000046"),
    InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDataObject
    {
        [PreserveSig()]
        uint GetData(
            ref FORMATETC a,
            ref STGMEDIUM b);

        [PreserveSig()]
        uint GetDataHere(
            ref FORMATETC pFormatetc,
            out STGMEDIUM pMedium);

        [PreserveSig()]
        uint QueryGetData(
            ref FORMATETC pFormatetc);

        [PreserveSig()]
        uint GetCanonicalFormatEtc(
            ref FORMATETC pformatectIn,
            out	FORMATETC pformatetcOut);

        [PreserveSig()]
        uint SetData(
            ref FORMATETC pFormatectIn,
            ref STGMEDIUM pmedium,
            [In, MarshalAs(UnmanagedType.Bool)]
			bool fRelease);

        [PreserveSig()]
        uint EnumFormatEtc(
            uint dwDirection, IEnumFORMATETC penum);

        [PreserveSig()]
        uint DAdvise(
            ref FORMATETC pFormatetc,
            int advf,
            [In, MarshalAs(UnmanagedType.Interface)]
			IAdviseSink pAdvSink,
            out uint pdwConnection);

        [PreserveSig()]
        uint DUnadvise(
            uint dwConnection);

        [PreserveSig()]
        uint EnumDAdvise(
            [Out, MarshalAs(UnmanagedType.Interface)]
			out IEnumSTATDATA ppenumAdvise);
    }

    [ComVisible(true), Guid("00000118-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IOleClientSite
    {

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int SaveObject();

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int GetMoniker(
            [In, MarshalAs(UnmanagedType.U4)]          int dwAssign,
            [In, MarshalAs(UnmanagedType.U4)]          int dwWhichMoniker,
            [Out, MarshalAs(UnmanagedType.Interface)] out object ppmk);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int GetContainer([MarshalAs(UnmanagedType.Interface)] out IOleContainer container);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int ShowObject();

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int OnShowWindow(
            [In, MarshalAs(UnmanagedType.I4)] int fShow);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int RequestNewObjectLayout();
    }

    [ComVisible(false), StructLayout(LayoutKind.Sequential)]
    public sealed class tagLOGPALETTE
    {
        [MarshalAs(UnmanagedType.U2)/*leftover(offset=0, palVersion)*/]
        public short palVersion;

        [MarshalAs(UnmanagedType.U2)/*leftover(offset=2, palNumEntries)*/]
        public short palNumEntries;

        // UNMAPPABLE: palPalEntry: Cannot be used as a structure field.
        //   /** @com.structmap(UNMAPPABLE palPalEntry) */
        //  public UNMAPPABLE palPalEntry;
    }

    [ComVisible(true), ComImport(), Guid("00000112-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IOleObject
    {

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int SetClientSite(
            [In, MarshalAs(UnmanagedType.Interface)]
			IOleClientSite pClientSite);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int GetClientSite(out IOleClientSite site);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int SetHostNames(
            [In, MarshalAs(UnmanagedType.LPWStr)]
			string szContainerApp,
            [In, MarshalAs(UnmanagedType.LPWStr)]
			string szContainerObj);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int Close(
            [In, MarshalAs(UnmanagedType.I4)]
			int dwSaveOption);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int SetMoniker(
            [In, MarshalAs(UnmanagedType.U4)]
			int dwWhichMoniker,
            [In, MarshalAs(UnmanagedType.Interface)]
			object pmk);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int GetMoniker(
            [In, MarshalAs(UnmanagedType.U4)]
			int dwAssign,
            [In, MarshalAs(UnmanagedType.U4)]
			int dwWhichMoniker,
            out object moniker);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int InitFromData(
            [In, MarshalAs(UnmanagedType.Interface)]
			IDataObject pDataObject,
            [In, MarshalAs(UnmanagedType.I4)]
			int fCreation,
            [In, MarshalAs(UnmanagedType.U4)]
			int dwReserved);

        int GetClipboardData(
            [In, MarshalAs(UnmanagedType.U4)]
			int dwReserved,
            out IDataObject data);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int DoVerb(
            [In, MarshalAs(UnmanagedType.I4)]
			int iVerb,
            [In]
			IntPtr lpmsg,
            [In, MarshalAs(UnmanagedType.Interface)]
			IOleClientSite pActiveSite,
            [In, MarshalAs(UnmanagedType.I4)]
			int lindex,
            [In]
			IntPtr hwndParent,
            [In]
			COMRECT lprcPosRect);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int EnumVerbs(out IEnumOLEVERB e);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int Update();

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int IsUpToDate();

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int GetUserClassID(
            [In, Out]
			ref Guid pClsid);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int GetUserType(
            [In, MarshalAs(UnmanagedType.U4)]
			int dwFormOfType,
            [Out, MarshalAs(UnmanagedType.LPWStr)]
			out string userType);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int SetExtent(
            [In, MarshalAs(UnmanagedType.U4)]
			int dwDrawAspect,
            [In]
			Size pSizel);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int GetExtent(
            [In, MarshalAs(UnmanagedType.U4)]
			int dwDrawAspect,
            [Out]
			Size pSizel);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int Advise([In, MarshalAs(UnmanagedType.Interface)] IAdviseSink pAdvSink, out int cookie);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int Unadvise([In, MarshalAs(UnmanagedType.U4)] int dwConnection);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int EnumAdvise(out IEnumSTATDATA e);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int GetMiscStatus([In, MarshalAs(UnmanagedType.U4)] int dwAspect, out int misc);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int SetColorScheme([In] tagLOGPALETTE pLogpal);
    }

    [ComImport]
    [Guid("0000000d-0000-0000-C000-000000000046")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IEnumSTATSTG
    {
        // The user needs to allocate an STATSTG array whose size is celt.
        [PreserveSig]
        uint
            Next(
            uint celt,
            [MarshalAs(UnmanagedType.LPArray), Out]
			STATSTG[] rgelt,
            out uint pceltFetched
            );

        void Skip(uint celt);

        void Reset();

        [return: MarshalAs(UnmanagedType.Interface)]
        IEnumSTATSTG Clone();
    }

    [ComImport]
    [Guid("0000000b-0000-0000-C000-000000000046")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IStorage
    {
        int CreateStream(
            /* [string][in] */ string pwcsName,
            /* [in] */ uint grfMode,
            /* [in] */ uint reserved1,
            /* [in] */ uint reserved2,
            /* [out] */ out IStream ppstm);

        int OpenStream(
            /* [string][in] */ string pwcsName,
            /* [unique][in] */ IntPtr reserved1,
            /* [in] */ uint grfMode,
            /* [in] */ uint reserved2,
            /* [out] */ out IStream ppstm);

        int CreateStorage(
            /* [string][in] */ string pwcsName,
            /* [in] */ uint grfMode,
            /* [in] */ uint reserved1,
            /* [in] */ uint reserved2,
            /* [out] */ out IStorage ppstg);

        int OpenStorage(
            /* [string][unique][in] */ string pwcsName,
            /* [unique][in] */ IStorage pstgPriority,
            /* [in] */ uint grfMode,
            /* [unique][in] */ IntPtr snbExclude,
            /* [in] */ uint reserved,
            /* [out] */ out IStorage ppstg);

        int CopyTo(
            /* [in] */ uint ciidExclude,
            /* [size_is][unique][in] */ Guid rgiidExclude,
            /* [unique][in] */ IntPtr snbExclude,
            /* [unique][in] */ IStorage pstgDest);

        int MoveElementTo(
            /* [string][in] */ string pwcsName,
            /* [unique][in] */ IStorage pstgDest,
            /* [string][in] */ string pwcsNewName,
            /* [in] */ uint grfFlags);

        int Commit(
            /* [in] */ uint grfCommitFlags);

        int Revert();

        int EnumElements(
            /* [in] */ uint reserved1,
            /* [size_is][unique][in] */ IntPtr reserved2,
            /* [in] */ uint reserved3,
            /* [out] */ out IEnumSTATSTG ppenum);

        int DestroyElement(
            /* [string][in] */ string pwcsName);

        int RenameElement(
            /* [string][in] */ string pwcsOldName,
            /* [string][in] */ string pwcsNewName);

        int SetElementTimes(
            /* [string][unique][in] */ string pwcsName,
            /* [unique][in] */ FILETIME pctime,
            /* [unique][in] */ FILETIME patime,
            /* [unique][in] */ FILETIME pmtime);

        int SetClass(
            /* [in] */ Guid clsid);

        int SetStateBits(
            /* [in] */ uint grfStateBits,
            /* [in] */ uint grfMask);

        int Stat(
            /* [out] */ out STATSTG pstatstg,
            /* [in] */ uint grfStatFlag);

    }

    [ComImport]
    [Guid("0000000a-0000-0000-C000-000000000046")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ILockBytes
    {
        int ReadAt(
            /* [in] */ ulong ulOffset,
            /* [unique][out] */ IntPtr pv,
            /* [in] */ uint cb,
            /* [out] */ out IntPtr pcbRead);

        int WriteAt(
            /* [in] */ ulong ulOffset,
            /* [size_is][in] */ IntPtr pv,
            /* [in] */ uint cb,
            /* [out] */ out IntPtr pcbWritten);

        int Flush();

        int SetSize(
            /* [in] */ ulong cb);

        int LockRegion(
            /* [in] */ ulong libOffset,
            /* [in] */ ulong cb,
            /* [in] */ uint dwLockType);

        int UnlockRegion(
            /* [in] */ ulong libOffset,
            /* [in] */ ulong cb,
            /* [in] */ uint dwLockType);

        int Stat(
            /* [out] */ out STATSTG pstatstg,
            /* [in] */ uint grfStatFlag);

    }

    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("0c733a30-2a1c-11ce-ade5-00aa0044773d")]
    public interface ISequentialStream
    {
        int Read(
            /* [length_is][size_is][out] */ IntPtr pv,
            /* [in] */ uint cb,
            /* [out] */ out uint pcbRead);

        int Write(
            /* [size_is][in] */ IntPtr pv,
            /* [in] */ uint cb,
            /* [out] */ out uint pcbWritten);

    };

    [ComImport]
    [Guid("0000000c-0000-0000-C000-000000000046")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IStream : ISequentialStream
    {
        int Seek(
            /* [in] */ ulong dlibMove,
            /* [in] */ uint dwOrigin,
            /* [out] */ out ulong plibNewPosition);

        int SetSize(
            /* [in] */ ulong libNewSize);

        int CopyTo(
            /* [unique][in] */ [In] IStream pstm,
            /* [in] */ ulong cb,
            /* [out] */ out ulong pcbRead,
            /* [out] */ out ulong pcbWritten);

        int Commit(
            /* [in] */ uint grfCommitFlags);

        int Revert();

        int LockRegion(
            /* [in] */ ulong libOffset,
            /* [in] */ ulong cb,
            /* [in] */ uint dwLockType);

        int UnlockRegion(
            /* [in] */ ulong libOffset,
            /* [in] */ ulong cb,
            /* [in] */ uint dwLockType);

        int Stat(
            /* [out] */ out STATSTG pstatstg,
            /* [in] */ uint grfStatFlag);

        int Clone(
            /* [out] */ out IStream ppstm);

    };

    /// <summary>
    /// Definition for interface IPersist.
    /// </summary>
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("0000010c-0000-0000-C000-000000000046")]
    public interface IPersist
    {
        /// <summary>
        /// getClassID
        /// </summary>
        /// <param name="pClassID"></param>
        void GetClassID( /* [out] */ out Guid pClassID);
    }

    /// <summary>
    /// Definition for interface IPersistStream.
    /// </summary>
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("00000109-0000-0000-C000-000000000046")]
    public interface IPersistStream : IPersist
    {
        /// <summary>
        /// GetClassID
        /// </summary>
        /// <param name="pClassID"></param>
        new void GetClassID(out Guid pClassID);
        /// <summary>
        /// isDirty
        /// </summary>
        /// <returns></returns>
        [PreserveSig]
        int IsDirty();
        /// <summary>
        /// Load
        /// </summary>
        /// <param name="pStm"></param>
        void Load([In] UCOMIStream pStm);
        /// <summary>
        /// Save
        /// </summary>
        /// <param name="pStm"></param>
        /// <param name="fClearDirty"></param>
        void Save([In] UCOMIStream pStm, [In, MarshalAs(UnmanagedType.Bool)] bool fClearDirty);
        /// <summary>
        /// GetSizeMax
        /// </summary>
        /// <param name="pcbSize"></param>
        void GetSizeMax(out long pcbSize);
    }

    [ComImport(), Guid("00020D00-0000-0000-c000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IRichEditOle
    {
        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int GetClientSite(out IOleClientSite site);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int GetObjectCount();

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int GetLinkCount();

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int GetObject(int iob, [In, Out] REOBJECT lpreobject, [MarshalAs(UnmanagedType.U4)]GETOBJECTOPTIONS flags);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int InsertObject(REOBJECT lpreobject);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int ConvertObject(int iob, Guid rclsidNew, string lpstrUserTypeNew);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int ActivateAs(Guid rclsid, Guid rclsidAs);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int SetHostNames(string lpstrContainerApp, string lpstrContainerObj);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int SetLinkAvailable(int iob, bool fAvailable);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int SetDvaspect(int iob, uint dvaspect);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int HandsOffStorage(int iob);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int SaveCompleted(int iob, IStorage lpstg);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int InPlaceDeactivate();

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int ContextSensitiveHelp(bool fEnterMode);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int GetClipboardData([In, Out] ref CHARRANGE lpchrg, [MarshalAs(UnmanagedType.U4)] GETCLIPBOARDDATAFLAGS reco, out IDataObject lplpdataobj);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int ImportDataObject(IDataObject lpdataobj, int cf, IntPtr hMetaPict);
    }


    [ComVisible(true), ComImport(), Guid("00000115-0000-0000-C000-000000000046"),
    InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IOleInPlaceUIWindow
    {
        //IOleWindow
        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int GetWindow([In, Out] ref IntPtr phwnd);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int ContextSensitiveHelp([In, MarshalAs(UnmanagedType.Bool)] bool fEnterMode);

        //IOleInPlaceUIWindow
        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int GetBorder([In, Out, MarshalAs(UnmanagedType.Struct)] ref RECT lprectBorder);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int RequestBorderSpace([In, MarshalAs(UnmanagedType.Struct)] ref RECT pborderwidths);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int SetBorderSpace([In, MarshalAs(UnmanagedType.Struct)] ref RECT pborderwidths);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int SetActiveObject([In, MarshalAs(UnmanagedType.Interface)] ref IOleInPlaceActiveObject pActiveObject, [In, MarshalAs(UnmanagedType.LPWStr)] string pszObjName);
    }

    [ComVisible(true), ComImport(), Guid("00000117-0000-0000-C000-000000000046"),
    InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IOleInPlaceActiveObject
    {
        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int GetWindow([In, Out] ref IntPtr phwnd);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int ContextSensitiveHelp([In, MarshalAs(UnmanagedType.Bool)] bool fEnterMode);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int TranslateAccelerator([In, MarshalAs(UnmanagedType.Struct)] ref  string lpmsg);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int OnFrameWindowActivate([In, MarshalAs(UnmanagedType.Bool)] bool fActivate);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int OnDocWindowActivate([In, MarshalAs(UnmanagedType.Bool)] bool fActivate);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int ResizeBorder([In, MarshalAs(UnmanagedType.Struct)] ref RECT prcBorder, [In, MarshalAs(UnmanagedType.Interface)] ref IOleInPlaceUIWindow pUIWindow, [In, MarshalAs(UnmanagedType.Bool)] bool fFrameWindow);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int EnableModeless([In, MarshalAs(UnmanagedType.Bool)] bool fEnable);
    }

    [ComVisible(true), ComImport(), Guid("00000116-0000-0000-C000-000000000046"),
    InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IOleInPlaceFrame
    {
        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int GetWindow([In, Out] ref IntPtr phwnd);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int ContextSensitiveHelp([In, MarshalAs(UnmanagedType.Bool)] bool fEnterMode);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int GetBorder([Out, MarshalAs(UnmanagedType.LPStruct)] RECT lprectBorder);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int RequestBorderSpace([In, MarshalAs(UnmanagedType.Struct)] ref RECT pborderwidths);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int SetBorderSpace([In, MarshalAs(UnmanagedType.Struct)] ref RECT pborderwidths);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int SetActiveObject([In, MarshalAs(UnmanagedType.Interface)] ref IOleInPlaceActiveObject pActiveObject, [In, MarshalAs(UnmanagedType.LPWStr)] string pszObjName);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int InsertMenus([In] IntPtr hmenuShared, [In, Out, MarshalAs(UnmanagedType.Struct)] ref object lpMenuWidths);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int SetMenu([In] IntPtr hmenuShared, [In] IntPtr holemenu, [In] IntPtr hwndActiveObject);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int RemoveMenus([In] IntPtr hmenuShared);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int SetStatusText([In, MarshalAs(UnmanagedType.LPWStr)] string pszStatusText);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int EnableModeless([In, MarshalAs(UnmanagedType.Bool)] bool fEnable);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int TranslateAccelerator([In, MarshalAs(UnmanagedType.Struct)] ref string lpmsg, [In, MarshalAs(UnmanagedType.U2)] short wID);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct OLEINPLACEFRAMEINFO
    {
        public uint cb;
        [MarshalAs(UnmanagedType.Bool)]
        public bool fMDIApp;
        public IntPtr hwndFrame;
        public IntPtr haccel;
        public uint cAccelEntries;
    }

    [ComVisible(true), ComImport(), Guid("00020D03-0000-0000-C000-000000000046"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IRichEditOleCallback
    {
        IStorage GetNewStorage();
        void GetInPlaceContext(IOleInPlaceFrame lplpFrame, IOleInPlaceUIWindow lplpDoc, ref OLEINPLACEFRAMEINFO lpFrameInfo);
        void ShowContainerUI([MarshalAs(UnmanagedType.Bool)] bool fShow);
        void QueryInsertObject(ref Guid lpclsid, IStorage lpstg, int cp);
        void DeleteObject(IOleObject lpoleobj);
        void QueryAcceptData(IDataObject lpdataobj, ref CLIPFORMAT
        lpcfFormat, uint reco, [MarshalAs(UnmanagedType.Bool)] bool fReally, IntPtr hMetaPict);
        void ContextSensitiveHelp([MarshalAs(UnmanagedType.Bool)] bool fEnterMode);
        IDataObject GetClipboardData(ref CHARRANGE lpchrg, uint reco);
        uint GetDragDropEffect([MarshalAs(UnmanagedType.Bool)] bool fDrag, uint grfKeyState);
        IntPtr GetContextMenu(ushort seltype, IOleObject lpoleobj, ref CHARRANGE lpchrg);
    }

    #endregion

    #region OLE API

    [DllImport("User32.dll", CharSet = CharSet.Auto, PreserveSig = false)]
    public static extern IRichEditOle SendMessage(IntPtr hWnd, int message, int wParam);

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
    internal static extern bool GetClientRect(IntPtr hWnd, [In, Out] ref Rectangle rect);

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
    internal static extern bool GetWindowRect(IntPtr hWnd, [In, Out] ref Rectangle rect);

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
    internal static extern IntPtr GetParent(IntPtr hWnd);

    [DllImport("ole32.dll")]
    static extern int OleSetContainedObject([MarshalAs(UnmanagedType.IUnknown)]
			object pUnk, bool fContained);

    [DllImport("ole32.dll")]
    static extern int OleLoadPicturePath(
        [MarshalAs(UnmanagedType.LPWStr)] string lpszPicturePath,
        [MarshalAs(UnmanagedType.IUnknown)][In] object pIUnknown,
        uint dwReserved,
        uint clrReserved,
        ref Guid riid,
        [MarshalAs(UnmanagedType.IUnknown)] out object ppvObj);

    [DllImport("ole32.dll")]
    static extern int OleCreateFromFile([In] ref Guid rclsid,
        [MarshalAs(UnmanagedType.LPWStr)] string lpszFileName, [In] ref Guid riid,
        uint renderopt, ref FORMATETC pFormatEtc, IOleClientSite pClientSite,
        IStorage pStg, [MarshalAs(UnmanagedType.IUnknown)] out object ppvObj);

    [DllImport("ole32.dll")]
    static extern int OleCreateFromData(IDataObject pSrcDataObj,
        [In] ref Guid riid, uint renderopt, ref FORMATETC pFormatEtc,
        IOleClientSite pClientSite, IStorage pStg,
        [MarshalAs(UnmanagedType.IUnknown)] out object ppvObj);

    [DllImport("ole32.dll")]
    static extern int OleCreateStaticFromData([MarshalAs(UnmanagedType.Interface)]IDataObject pSrcDataObj,
        [In] ref Guid riid, uint renderopt, ref FORMATETC pFormatEtc,
        IOleClientSite pClientSite, IStorage pStg,
        [MarshalAs(UnmanagedType.IUnknown)] out object ppvObj);

    [DllImport("ole32.dll")]
    static extern int OleCreateLinkFromData([MarshalAs(UnmanagedType.Interface)]IDataObject pSrcDataObj,
        [In] ref Guid riid, uint renderopt, ref FORMATETC pFormatEtc,
        IOleClientSite pClientSite, IStorage pStg,
        [MarshalAs(UnmanagedType.IUnknown)] out object ppvObj);

    [DllImport("ole32.dll", PreserveSig = false)]
    internal static extern int CreateILockBytesOnHGlobal(IntPtr hGlobal, bool fDeleteOnRelease, [Out] out ILockBytes ppLkbyt);

    [DllImport("ole32.dll")]
    static extern int StgCreateDocfileOnILockBytes(ILockBytes plkbyt, uint grfMode,
        uint reserved, out IStorage ppstgOpen);


    #endregion

    public class DataObject : IDataObject
    {
        private Bitmap mBitmap;
        public FORMATETC mpFormatetc;

        #region IDataObject Members

        private const uint S_OK = 0;
        private const uint E_POINTER = 0x80004003;
        private const uint E_NOTIMPL = 0x80004001;
        private const uint E_FAIL = 0x80004005;

        public uint GetData(ref FORMATETC pFormatetc, ref STGMEDIUM pMedium)
        {
            IntPtr hDst = mBitmap.GetHbitmap();

            pMedium.tymed = (int)TYMED.TYMED_GDI;
            pMedium.unionmember = hDst;
            pMedium.pUnkForRelease = IntPtr.Zero;

            return (uint)S_OK;
        }

        public uint GetDataHere(ref FORMATETC pFormatetc, out STGMEDIUM pMedium)
        {
            Trace.WriteLine("GetDataHere");

            pMedium = new STGMEDIUM();

            return (uint)E_NOTIMPL;
        }

        public uint QueryGetData(ref FORMATETC pFormatetc)
        {
            Trace.WriteLine("QueryGetData");

            return (uint)E_NOTIMPL;
        }

        public uint GetCanonicalFormatEtc(ref FORMATETC pFormatetcIn, out FORMATETC pFormatetcOut)
        {
            Trace.WriteLine("GetCanonicalFormatEtc");

            pFormatetcOut = new FORMATETC();

            return (uint)E_NOTIMPL;
        }

        public uint SetData(ref FORMATETC a, ref STGMEDIUM b, bool fRelease)
        {
            //mpFormatetc = pFormatectIn;
            //mpmedium = pmedium;

            Trace.WriteLine("SetData");

            return (int)S_OK;
        }

        public uint EnumFormatEtc(uint dwDirection, IEnumFORMATETC penum)
        {
            Trace.WriteLine("EnumFormatEtc");

            return (int)S_OK;
        }

        public uint DAdvise(ref FORMATETC a, int advf, IAdviseSink pAdvSink, out uint pdwConnection)
        {
            Trace.WriteLine("DAdvise");

            pdwConnection = 0;

            return (uint)E_NOTIMPL;
        }

        public uint DUnadvise(uint dwConnection)
        {
            Trace.WriteLine("DUnadvise");

            return (uint)E_NOTIMPL;
        }

        public uint EnumDAdvise(out IEnumSTATDATA ppenumAdvise)
        {
            Trace.WriteLine("EnumDAdvise");

            ppenumAdvise = null;

            return (uint)E_NOTIMPL;
        }

        #endregion

        public DataObject()
        {
            mBitmap = new Bitmap(16, 16);
            mpFormatetc = new FORMATETC();
        }

        public void SetImage(string strFilename)
        {
            try
            {
                mBitmap = (Bitmap)Bitmap.FromFile(strFilename, true);

                mpFormatetc.cfFormat = CLIPFORMAT.CF_BITMAP;				// Clipboard format = CF_BITMAP
                mpFormatetc.ptd = IntPtr.Zero;							// Target Device = Screen
                mpFormatetc.dwAspect = DVASPECT.DVASPECT_CONTENT;			// Level of detail = Full content
                mpFormatetc.lindex = -1;							// Index = Not applicaple
                mpFormatetc.tymed = TYMED.TYMED_GDI;					// Storage medium = HBITMAP handle
            }
            catch
            {
            }
        }

        public void SetImage(Image image)
        {
            try
            {
                mBitmap = new Bitmap(image);

                mpFormatetc.cfFormat = CLIPFORMAT.CF_BITMAP;				// Clipboard format = CF_BITMAP
                mpFormatetc.ptd = IntPtr.Zero;							// Target Device = Screen
                mpFormatetc.dwAspect = DVASPECT.DVASPECT_CONTENT;			// Level of detail = Full content
                mpFormatetc.lindex = -1;							// Index = Not applicaple
                mpFormatetc.tymed = TYMED.TYMED_GDI;					// Storage medium = HBITMAP handle
            }
            catch
            {
            }
        }
    }

    // RichEditOle wrapper and helper
    private class RichEditOle
    {
        public const int WM_USER = 0x0400;
        public const int EM_GETOLEINTERFACE = WM_USER + 60;

        private ExtendedRichTextBox _richEdit;
        private IRichEditOle _RichEditOle;

        public RichEditOle(ExtendedRichTextBox richEdit)
        {
            this._richEdit = richEdit;
        }

        private IRichEditOle IRichEditOle
        {
            get
            {
                if (this._RichEditOle == null)
                {
                    this._RichEditOle = SendMessage(this._richEdit.Handle, EM_GETOLEINTERFACE, 0);
                }

                return this._RichEditOle;
            }
        }

        [DllImport("ole32.dll", PreserveSig = false)]
        internal static extern int CreateILockBytesOnHGlobal(IntPtr hGlobal, bool fDeleteOnRelease, [Out] out ILockBytes ppLkbyt);

        [DllImport("ole32.dll")]
        static extern int StgCreateDocfileOnILockBytes(ILockBytes plkbyt, uint grfMode,
            uint reserved, out IStorage ppstgOpen);

        public REOBJECT GetObjectByIndex(int objIndex)
        {
            try
            {
                REOBJECT obj = new REOBJECT();
                this.IRichEditOle.GetObject(objIndex, obj, GETOBJECTOPTIONS.REO_GETOBJ_ALL_INTERFACES);
                return obj;
            }
            catch (Exception) // it seems that invalid index was specified
            {
                return null;
            }
        }

        public REOBJECT GetSelectedObject()
        {
            REOBJECT obj = new REOBJECT();
            this.IRichEditOle.GetObject(-1, obj, GETOBJECTOPTIONS.REO_GETOBJ_ALL_INTERFACES);
            return obj;
        }

        public void InsertControl(Control control)
        {
            if (control == null)
                return;

            Guid guid = Marshal.GenerateGuidForType(control.GetType());

            //-----------------------
            ILockBytes pLockBytes;
            CreateILockBytesOnHGlobal(IntPtr.Zero, true, out pLockBytes);

            IStorage pStorage;
            StgCreateDocfileOnILockBytes(pLockBytes, (uint)(STGM.STGM_SHARE_EXCLUSIVE | STGM.STGM_CREATE | STGM.STGM_READWRITE), 0, out pStorage);

            IOleClientSite pOleClientSite;
            this.IRichEditOle.GetClientSite(out pOleClientSite);
            //-----------------------

            //-----------------------
            REOBJECT reoObject = new REOBJECT();

            reoObject.cp = this._richEdit.TextLength;

            reoObject.clsid = guid;
            reoObject.pstg = pStorage;
            reoObject.poleobj = Marshal.GetIUnknownForObject(control);
            reoObject.polesite = pOleClientSite;
            reoObject.dvAspect = (uint)(DVASPECT.DVASPECT_CONTENT);
            reoObject.dwFlags = (uint)(REOOBJECTFLAGS.REO_BELOWBASELINE | REOOBJECTFLAGS.REO_RESIZABLE);
            reoObject.dwUser = 1;

            this.IRichEditOle.InsertObject(reoObject);
            //-----------------------

            //-----------------------
            Marshal.ReleaseComObject(pLockBytes);
            Marshal.ReleaseComObject(pOleClientSite);
            Marshal.ReleaseComObject(pStorage);
            //-----------------------
        }

        public bool InsertImageFromFile(string strFilename)
        {
            //-----------------------
            ILockBytes pLockBytes;
            CreateILockBytesOnHGlobal(IntPtr.Zero, true, out pLockBytes);

            IStorage pStorage;
            StgCreateDocfileOnILockBytes(pLockBytes, (uint)(STGM.STGM_SHARE_EXCLUSIVE | STGM.STGM_CREATE | STGM.STGM_READWRITE), 0, out pStorage);

            IOleClientSite pOleClientSite;
            this.IRichEditOle.GetClientSite(out pOleClientSite);
            //-----------------------


            //-----------------------
            FORMATETC formatEtc = new FORMATETC();

            formatEtc.cfFormat = 0;
            formatEtc.ptd = IntPtr.Zero;
            formatEtc.dwAspect = DVASPECT.DVASPECT_CONTENT;
            formatEtc.lindex = -1;
            formatEtc.tymed = TYMED.TYMED_NULL;

            Guid IID_IOleObject = new Guid("{00000112-0000-0000-C000-000000000046}");
            Guid CLSID_NULL = new Guid("{00000000-0000-0000-0000-000000000000}");

            object pOleObjectOut;

            // I don't sure, but it appears that this function only loads from bitmap
            // You can also try OleCreateFromData, OleLoadPictureIndirect, etc.
            int hr = OleCreateFromFile(ref CLSID_NULL, strFilename, ref IID_IOleObject, (uint)OLERENDER.OLERENDER_DRAW, ref formatEtc, pOleClientSite, pStorage, out pOleObjectOut);

            if (pOleObjectOut == null)
            {
                Marshal.ReleaseComObject(pLockBytes);
                Marshal.ReleaseComObject(pOleClientSite);
                Marshal.ReleaseComObject(pStorage);

                return false;
            }

            IOleObject pOleObject = (IOleObject)pOleObjectOut;
            //-----------------------


            //-----------------------
            Guid guid = new Guid();

            //guid = Marshal.GenerateGuidForType(pOleObject.GetType());
            pOleObject.GetUserClassID(ref guid);
            //-----------------------

            //-----------------------
            OleSetContainedObject(pOleObject, true);

            REOBJECT reoObject = new REOBJECT();

            reoObject.cp = this._richEdit.TextLength;

            reoObject.clsid = guid;
            reoObject.pstg = pStorage;
            reoObject.poleobj = Marshal.GetIUnknownForObject(pOleObject);
            reoObject.polesite = pOleClientSite;
            reoObject.dvAspect = (uint)(DVASPECT.DVASPECT_CONTENT);
            reoObject.dwFlags = (uint)(REOOBJECTFLAGS.REO_BELOWBASELINE | REOOBJECTFLAGS.REO_RESIZABLE);
            reoObject.dwUser = 0;

            this.IRichEditOle.InsertObject(reoObject);
            //-----------------------

            //-----------------------
            Marshal.ReleaseComObject(pLockBytes);
            Marshal.ReleaseComObject(pOleClientSite);
            Marshal.ReleaseComObject(pStorage);
            Marshal.ReleaseComObject(pOleObject);
            //-----------------------

            return true;
        }

        public void InsertMyDataObject(DataObject dobj)
        {
            if (dobj == null)
                return;

            //-----------------------
            ILockBytes pLockBytes;
            int sc = CreateILockBytesOnHGlobal(IntPtr.Zero, true, out pLockBytes);

            IStorage pStorage;
            sc = StgCreateDocfileOnILockBytes(pLockBytes, (uint)(STGM.STGM_SHARE_EXCLUSIVE | STGM.STGM_CREATE | STGM.STGM_READWRITE), 0, out pStorage);

            IOleClientSite pOleClientSite;
            this.IRichEditOle.GetClientSite(out pOleClientSite);
            //-----------------------

            Guid guid = Marshal.GenerateGuidForType(dobj.GetType());

            Guid IID_IOleObject = new Guid("{00000112-0000-0000-C000-000000000046}");
            Guid IID_IDataObject = new Guid("{0000010e-0000-0000-C000-000000000046}");
            Guid IID_IUnknown = new Guid("{00000000-0000-0000-C000-000000000046}");

            object pOleObject;

            int hr = OleCreateStaticFromData(dobj, ref IID_IOleObject, (uint)OLERENDER.OLERENDER_FORMAT, ref dobj.mpFormatetc, pOleClientSite, pStorage, out pOleObject);

            if (pOleObject == null)
                return;
            //-----------------------


            //-----------------------
            OleSetContainedObject(pOleObject, true);

            REOBJECT reoObject = new REOBJECT();

            reoObject.cp = this._richEdit.TextLength;

            reoObject.clsid = guid;
            reoObject.pstg = pStorage;
            reoObject.poleobj = Marshal.GetIUnknownForObject(pOleObject);
            reoObject.polesite = pOleClientSite;
            reoObject.dvAspect = (uint)(DVASPECT.DVASPECT_CONTENT);
            reoObject.dwFlags = (uint)(REOOBJECTFLAGS.REO_BELOWBASELINE | REOOBJECTFLAGS.REO_RESIZABLE);
            reoObject.dwUser = 0;

            this.IRichEditOle.InsertObject(reoObject);
            //-----------------------

            //-----------------------
            Marshal.ReleaseComObject(pLockBytes);
            Marshal.ReleaseComObject(pOleClientSite);
            Marshal.ReleaseComObject(pStorage);
            Marshal.ReleaseComObject(pOleObject);
            //-----------------------
        }

        public void InsertOleObject(IOleObject oleObject)
        {
            if (oleObject == null)
                return;

            //-----------------------
            ILockBytes pLockBytes;
            CreateILockBytesOnHGlobal(IntPtr.Zero, true, out pLockBytes);

            IStorage pStorage;
            StgCreateDocfileOnILockBytes(pLockBytes, (uint)(STGM.STGM_SHARE_EXCLUSIVE | STGM.STGM_CREATE | STGM.STGM_READWRITE), 0, out pStorage);

            IOleClientSite pOleClientSite;
            this.IRichEditOle.GetClientSite(out pOleClientSite);
            //-----------------------

            //-----------------------
            Guid guid = new Guid();

            oleObject.GetUserClassID(ref guid);
            //-----------------------

            //-----------------------
            OleSetContainedObject(oleObject, true);

            REOBJECT reoObject = new REOBJECT();

            reoObject.cp = this._richEdit.TextLength;

            reoObject.clsid = guid;
            reoObject.pstg = pStorage;
            reoObject.poleobj = Marshal.GetIUnknownForObject(oleObject);
            reoObject.polesite = pOleClientSite;
            reoObject.dvAspect = (uint)DVASPECT.DVASPECT_CONTENT;
            reoObject.dwFlags = (uint)(REOOBJECTFLAGS.REO_BELOWBASELINE | REOOBJECTFLAGS.REO_RESIZABLE);

            this.IRichEditOle.InsertObject(reoObject);
            //-----------------------

            //-----------------------
            Marshal.ReleaseComObject(pLockBytes);
            Marshal.ReleaseComObject(pOleClientSite);
            Marshal.ReleaseComObject(pStorage);
            //-----------------------
        }

        public void UpdateObjects()
        {
            int k = this.IRichEditOle.GetObjectCount();

            if (k == 0) return;

            for (int i = 0; i < k; i++)
            {
                REOBJECT reoObject = new REOBJECT();

                this.IRichEditOle.GetObject(i, reoObject, GETOBJECTOPTIONS.REO_GETOBJ_ALL_INTERFACES);

                if (reoObject.dwUser == 1)
                {
                    Point pt = this._richEdit.GetPositionFromCharIndex(reoObject.cp);
                    Rectangle rect = new Rectangle(pt, reoObject.sizel);

                    this._richEdit.Invalidate(rect, false); // repaint
                }
            }
        }
    }

    public void InsertOleObject(IOleObject oleObj)
    {
        RichEditOle ole = new RichEditOle(this);
        ole.InsertOleObject(oleObj);
    }

    public void InsertControl(Control control)
    {
        RichEditOle ole = new RichEditOle(this);
        ole.InsertControl(control);
    }

    public void InsertMyDataObject(DataObject dobj)
    {
        RichEditOle ole = new RichEditOle(this);
        ole.InsertMyDataObject(dobj);
    }

    public void UpdateObjects()
    {
        RichEditOle ole = new RichEditOle(this);
        ole.UpdateObjects();
    }

    public void InsertImage(Image image)
    {
        DataObject dobj = new DataObject();

        dobj.SetImage(image);

        this.InsertMyDataObject(dobj);
    }

    public void InsertImage(string imageFile)
    {
        DataObject dobj = new DataObject();

        dobj.SetImage(imageFile);

        this.InsertMyDataObject(dobj);
    }

    public void InsertImageFromFile(string strFilename)
    {
        RichEditOle ole = new RichEditOle(this);
        ole.InsertImageFromFile(strFilename);
    }

    public void InsertActiveX(string strProgID)
    {
        Type t = Type.GetTypeFromProgID(strProgID);
        if (t == null)
            return;

        object o = System.Activator.CreateInstance(t);

        bool b = (o is IOleObject);

        if (b)
            this.InsertOleObject((IOleObject)o);
    }

    public REOBJECT SelectedObject()
    {
        RichEditOle ole = new RichEditOle(this);
        return ole.GetSelectedObject();
    }

    #endregion

    #region ADDITIONAL PROCESSORS

    protected override void OnDragEnter(DragEventArgs drgevent)
    {
        //base.OnDragEnter(drgevent);

        if (drgevent.Data.GetDataPresent(DataFormats.Bitmap))
        {
            drgevent.Effect = DragDropEffects.Copy;
        }
        else if (drgevent.Data.GetDataPresent(DataFormats.FileDrop))
        {
            drgevent.Effect = DragDropEffects.Move;
            Array a = (Array)drgevent.Data.GetData(DataFormats.FileDrop);
            dropPath = a.GetValue(0).ToString();            
        }
    }
    protected override void OnDragOver(DragEventArgs drgevent)
    {
        //base.OnDragOver(drgevent);

        if (drgevent.Data.GetDataPresent(DataFormats.Bitmap))
        {
            drgevent.Effect = DragDropEffects.Copy;
        }
        else if (drgevent.Data.GetDataPresent(DataFormats.FileDrop))
        {
            drgevent.Effect = DragDropEffects.Move;
        }
    }
    bool IsImageExtention(string ext)
    {
        try
        {
            if (ext == ".png" || ext == ".bmp" || ext == ".jpg" || ext == ".jpeg" ||
                ext == ".gif" || ext == ".tif" || ext == ".tiff" || ext == ".wmf" ||
                ext == ".emf")
            {
                return true;
            }

            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }
    protected override void OnDragDrop(DragEventArgs drgevent)
    {
        if (drgevent.Data.GetDataPresent(DataFormats.Bitmap))
        {
            drgevent.Effect = DragDropEffects.Copy;
            DoDragDrop(drgevent.Data.GetData(DataFormats.Bitmap), DragDropEffects.Copy);
        }
        else if (drgevent.Data.GetDataPresent(DataFormats.FileDrop))
        {
            try
            {
                if (dropPath.Length == 0) return;

                if (System.IO.File.Exists(dropPath))
                {
                    System.IO.FileInfo fi = new System.IO.FileInfo(dropPath);
                    if (fi.Extension.ToLower() == ".rtf")
                    {                        
                        this.LoadFile(dropPath); drgevent.Data.SetData(null); drgevent = null; return;
                    }
                    else if (IsImageExtention(fi.Extension.ToLower()))
                    {
                        this.InsertImage(dropPath); drgevent.Data.SetData(null); drgevent = null; return;
                    }
                }
            }
            catch (Exception) { }
        }
        dropPath = "";
        //base.OnDragDrop(drgevent);
    }
    protected override void OnDragLeave(EventArgs e)
    {
        base.OnDragLeave(e);
        dropPath = "";
    }

    #endregion
}