using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;
using WMPLib; //right click your C# project, add reference, and add Windows Media Player



namespace Discord_WMP {
    //COM interfaces.cs bellow
    #region Useful COM Enums
    /// <summary>
    /// Represents a collection of frequently used HRESULT values.
    /// You may add more HRESULT VALUES, I've only included the ones used 
    /// in this project.
    /// </summary>
    public enum HResults {
        /// <summary>
        /// HRESULT S_OK
        /// </summary>
        S_OK = unchecked((int)0x00000000),
        /// <summary>
        /// HRESULT S_FALSE
        /// </summary>
        S_FALSE = unchecked((int)0x00000001),
        /// <summary>
        /// HRESULT E_NOINTERFACE
        /// </summary>
        E_NOINTERFACE = unchecked((int)0x80004002),
        /// <summary>
        /// HRESULT E_NOTIMPL
        /// </summary>
        E_NOTIMPL = unchecked((int)0x80004001),
        /// <summary>
        /// USED CLICKED CANCEL AT SAVE PROMPT
        /// </summary>
        OLE_E_PROMPTSAVECANCELLED = unchecked((int)0x8004000C),

    }

    /// <summary>
    /// Enumeration for <see cref="IOleObject.GetMiscStatus"/>
    /// </summary>
    public enum DVASPECT {
        /// <summary>
        /// See MSDN for more information.
        /// </summary>
        Content = 1,
        /// <summary>
        /// See MSDN for more information.
        /// </summary>
        Thumbnail = 2,
        /// <summary>
        /// See MSDN for more information.
        /// </summary>
        Icon = 3,
        /// <summary>
        /// See MSDN for more information.
        /// </summary>
        DocPrint = 4
    }
    /// <summary>
    /// Emumeration for <see cref="IOleObject.Close"/>
    /// </summary>
    public enum TAGOLECLOSE : uint {
        OLECLOSE_SAVEIFDIRTY = unchecked((int)0),
        OLECLOSE_NOSAVE = unchecked((int)1),
        OLECLOSE_PROMPTSAVE = unchecked((int)2)
    }

    #endregion

    #region IWMPRemoteMediaServices
    /// <summary>
    /// Interface used by Media Player to determine WMP Remoting status.
    /// </summary>
    [ComImport,
    ComVisible(true),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    Guid("CBB92747-741F-44fe-AB5B-F1A48F3B2A59")]
    public interface IWMPRemoteMediaServices {

        /// <summary>
        /// Service type.
        /// </summary>
        /// <returns><code>Remote</code> if the control is to be remoted (attached to WMP.) 
        /// <code>Local</code>if this is an independent WMP instance not connected to WMP application.  If you want local, you shouldn't bother
        /// using this control!
        /// </returns>
        [return: MarshalAs(UnmanagedType.BStr)]
        string GetServiceType();

        /// <summary>
        /// Value to display in Windows Media Player Switch To Application menu option (under View.)
        /// </summary>
        /// <returns></returns>
        [return: MarshalAs(UnmanagedType.BStr)]
        string GetApplicationName();

        /// <summary>
        /// Not in use, see MSDN for details.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dispatch"></param>
        /// <returns></returns>
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.U4)]
        HResults GetScriptableObject([MarshalAs(UnmanagedType.BStr)] out string name,
            [MarshalAs(UnmanagedType.IDispatch)] out object dispatch);

        /// <summary>
        /// Not in use, see MSDN for details.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.U4)]
        HResults GetCustomUIMode([MarshalAs(UnmanagedType.BStr)] out string file);
    }

    #endregion

    #region IOleServiceProvider
    /// <summary>
    /// Interface used by Windows Media Player to return an instance of IWMPRemoteMediaServices.
    /// </summary>
    [ComImport,
    GuidAttribute("6d5140c1-7436-11ce-8034-00aa006009fa"),
    InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown),
    ComVisible(true)]
    public interface IOleServiceProvider {
        /// <summary>
        /// Similar to QueryInterface, riid will contain the Guid of an object to return.
        /// In our project we will look for <see cref="IWMPRemoteMediaServices"/> Guid and return the object
        /// that implements that interface.
        /// </summary>
        /// <param name="guidService"></param>
        /// <param name="riid">The Guid of the desired Service to provide.</param>
        /// <returns>A pointer to the interface requested by the Guid.</returns>
        IntPtr QueryService(ref Guid guidService, ref Guid riid);
    }

    /// <summary>
    /// This is an example of an INCORRECT entry - do not use, unless you want your app to break.
    /// </summary>
    [ComImport,
    GuidAttribute("6d5140c1-7436-11ce-8034-00aa006009fa"),
    InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown),
    ComVisible(true)]
    public interface BadIOleServiceProvider {
        /// <summary>
        /// This is incorrect because it causes our return interface to be boxed
        /// as an object and a COM callee may not get the correct pointer.
        /// </summary>
        /// <param name="guidService"></param>
        /// <param name="riid"></param>
        /// <returns></returns>
        /// <example>
        /// For an example of a correct definition, look at <see cref="IOleServiceProvider"/>.
        /// </example>
        [return: MarshalAs(UnmanagedType.Interface)]
        object QueryService(ref Guid guidService, ref Guid riid);
    }
    #endregion

    #region IOleClientSite
    /// <summary>
    /// Need to implement this interface so we can pass it to <see cref="IOleObject.SetClientSite"/>.
    /// All functions return E_NOTIMPL.  We don't need to actually implement anything to get
    /// the remoting to work.
    /// </summary>
    [ComImport,
    ComVisible(true),
    Guid("00000118-0000-0000-C000-000000000046"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IOleClientSite {
        /// <summary>
        /// See MSDN for more information.  Throws <see cref="COMException"/> with id of E_NOTIMPL.
        /// </summary>
        /// <exception cref="COMException">E_NOTIMPL</exception>
        void SaveObject();

        /// <summary>
        /// See MSDN for more information.  Throws <see cref="COMException"/> with id of E_NOTIMPL.
        /// </summary>
        /// <exception cref="COMException">E_NOTIMPL</exception>
        [return: MarshalAs(UnmanagedType.Interface)]
        object GetMoniker(uint dwAssign, uint dwWhichMoniker);

        /// <summary>
        /// See MSDN for more information.  Throws <see cref="COMException"/> with id of E_NOTIMPL.
        /// </summary>
        /// <exception cref="COMException">E_NOTIMPL</exception>
        [return: MarshalAs(UnmanagedType.Interface)]
        object GetContainer();

        /// <summary>
        /// See MSDN for more information.  Throws <see cref="COMException"/> with id of E_NOTIMPL.
        /// </summary>
        /// <exception cref="COMException">E_NOTIMPL</exception>
        void ShowObject();

        /// <summary>
        /// See MSDN for more information.  Throws <see cref="COMException"/> with id of E_NOTIMPL.
        /// </summary>
        /// <exception cref="COMException">E_NOTIMPL</exception>
        void OnShowWindow(bool fShow);

        /// <summary>
        /// See MSDN for more information.  Throws <see cref="COMException"/> with id of E_NOTIMPL.
        /// </summary>
        /// <exception cref="COMException">E_NOTIMPL</exception>
        void RequestNewObjectLayout();
    }
    #endregion

    #region IOleObject
    /// <summary>
    /// This interface is implemented by WMP ActiveX/COM control.
    /// The only function we need is <see cref="SetClientSite"/>.
    /// </summary>
    [ComImport, ComVisible(true),
    Guid("00000112-0000-0000-C000-000000000046"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IOleObject {
        /// <summary>
        /// Used to pass our custom <see cref="IOleClientSite"/> object to WMP.  The object we pass must also
        /// implement <see cref="IOleServiceProvider"/> to work right.
        /// </summary>
        /// <param name="pClientSite">The <see cref="IOleClientSite"/> to pass.</param>
        void SetClientSite(IOleClientSite pClientSite);

        /// <summary>
        /// Implemented by Windows Media Player ActiveX control.
        /// See MSDN for more information.
        /// </summary>
        [return: MarshalAs(UnmanagedType.Interface)]
        IOleClientSite GetClientSite();

        /// <summary>
        /// Implemented by Windows Media Player ActiveX control.
        /// See MSDN for more information.
        /// </summary>
        void SetHostNames(
            [MarshalAs(UnmanagedType.LPWStr)] string szContainerApp,
            [MarshalAs(UnmanagedType.LPWStr)] string szContainerObj);

        /// <summary>
        /// Implemented by Windows Media Player ActiveX control.
        /// See MSDN for more information.
        /// </summary>
        void Close(uint dwSaveOption);

        /// <summary>
        /// Implemented by Windows Media Player ActiveX control.
        /// See MSDN for more information.
        /// </summary>
        void SetMoniker(uint dwWhichMoniker, object pmk);

        /// <summary>
        /// Implemented by Windows Media Player ActiveX control.
        /// See MSDN for more information.
        /// </summary>
        [return: MarshalAs(UnmanagedType.Interface)]
        object GetMoniker(uint dwAssign, uint dwWhichMoniker);

        /// <summary>
        /// Implemented by Windows Media Player ActiveX control.
        /// See MSDN for more information.
        /// </summary>
        void InitFromData(object pDataObject, bool fCreation, uint dwReserved);

        /// <summary>
        /// Implemented by Windows Media Player ActiveX control.
        /// See MSDN for more information.
        /// </summary>
        object GetClipboardData(uint dwReserved);


        /// <summary>
        /// Implemented by Windows Media Player ActiveX control.
        /// See MSDN for more information.
        /// </summary>
        void DoVerb(uint iVerb, uint lpmsg, [MarshalAs(UnmanagedType.Interface)] object pActiveSite,
            uint lindex, uint hwndParent, uint lprcPosRect);

        /// <summary>
        /// Implemented by Windows Media Player ActiveX control.
        /// See MSDN for more information.
        /// </summary>
        [return: MarshalAs(UnmanagedType.Interface)]
        object EnumVerbs();

        /// <summary>
        /// Implemented by Windows Media Player ActiveX control.
        /// See MSDN for more information.
        /// </summary>
        void Update();

        /// <summary>
        /// Implemented by Windows Media Player ActiveX control.
        /// See MSDN for more information.
        /// </summary>
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.U4)]
        HResults IsUpToDate();

        /// <summary>
        /// Implemented by Windows Media Player ActiveX control.
        /// See MSDN for more information.
        /// </summary>
        Guid GetUserClassID();

        /// <summary>
        /// Implemented by Windows Media Player ActiveX control.
        /// See MSDN for more information.
        /// </summary>
        [return: MarshalAs(UnmanagedType.LPWStr)]
        string GetUserType(uint dwFormOfType);

        /// <summary>
        /// Implemented by Windows Media Player ActiveX control.
        /// See MSDN for more information.
        /// </summary>
        void SetExtent(uint dwDrawAspect, [MarshalAs(UnmanagedType.Interface)] object psizel);

        /// <summary>
        /// Implemented by Windows Media Player ActiveX control.
        /// See MSDN for more information.
        /// </summary>
        [return: MarshalAs(UnmanagedType.Interface)]
        object GetExtent(uint dwDrawAspect);

        /// <summary>
        /// Implemented by Windows Media Player ActiveX control.
        /// See MSDN for more information.
        /// </summary>
        uint Advise([MarshalAs(UnmanagedType.Interface)] object pAdvSink);

        /// <summary>
        /// Implemented by Windows Media Player ActiveX control.
        /// See MSDN for more information.
        /// </summary>
        void Unadvise(uint dwConnection);

        /// <summary>
        /// Implemented by Windows Media Player ActiveX control.
        /// See MSDN for more information.
        /// </summary>
        [return: MarshalAs(UnmanagedType.Interface)]
        object EnumAdvise();

        /// <summary>
        /// Implemented by Windows Media Player ActiveX control.
        /// See MSDN for more information.
        /// </summary>
        uint GetMiscStatus([MarshalAs(UnmanagedType.U4)] DVASPECT dwAspect);

        /// <summary>
        /// Implemented by Windows Media Player ActiveX control.
        /// See MSDN for more information.
        /// </summary>
        void SetColorScheme([MarshalAs(UnmanagedType.Interface)] object pLogpal);
    }
    #endregion
    
    //RemoteHostInfo.cs bellow

    /// <summary>
    /// This class contains the information to return to Media Player about our remote service.
    /// </summary>
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    public class RemoteHostInfo :
        IWMPRemoteMediaServices {
        #region IWMPRemoteMediaServices Members
        /// <summary>
        /// Returns "Remote" to tell media player that we want to remote the WMP application.
        /// </summary>
        /// <returns></returns>
        public string GetServiceType() {
            return "Remote";
        }

        /// <summary>
        /// The Application Name to show in Windows Media Player switch to menu
        /// </summary>
        /// <returns></returns>
        public string GetApplicationName() {
            return System.Diagnostics.Process.GetCurrentProcess().ProcessName;
        }

        /// <summary>
        /// Not in use, see MSDN for more info.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dispatch"></param>
        /// <returns></returns>
        public HResults GetScriptableObject(out string name, out object dispatch) {
            name = null;
            dispatch = null;

            //return (int) HResults.S_OK;//NotImplemented
            return HResults.E_NOTIMPL;
        }

        /// <summary>
        /// For skins, not in use, see MSDN for more info.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public HResults GetCustomUIMode(out string file) {
            file = null;

            return HResults.E_NOTIMPL;//NotImplemented
        }

        #endregion
    }


    //RemotedWindowsMediaPlayer.cs bellow
    /// <summary>
    /// This is the actual Windows Media Control.
    /// </summary>
    [System.Windows.Forms.AxHost.ClsidAttribute("{6bf52a52-394a-11d3-b153-00c04f79faa6}")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class RemotedWindowsMediaPlayer : System.Windows.Forms.AxHost,
        IOleServiceProvider,
        IOleClientSite {
        
        /// <summary>
        /// Used to attach the appropriate interface to Windows Media Player.
        /// In here, we call SetClientSite on the WMP Control, passing it
        /// the dotNet container (this instance.)
        /// </summary>
        protected override void AttachInterfaces() {

            try {
                //Get the IOleObject for Windows Media Player.
                IOleObject oleObject = this.GetOcx() as IOleObject;

                if(oleObject != null) {
                    //Set the Client Site for the WMP control.
                    oleObject.SetClientSite(this as IOleClientSite);

                    // Try and get the OCX as a WMP player
                    if(this.GetOcx() as IWMPPlayer4 == null) {
                        throw new Exception(string.Format("OCX is not an IWMPPlayer4! GetType returns '{0}'",
                                                          this.GetOcx().GetType()));
                    }
                }
                else {
                    throw new Exception("Failed to get WMP OCX as an IOleObject?!");
                }

                return;
            }
            catch(System.Exception ex) {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }


        #region IOleServiceProvider Memebers - Working
        /// <summary>
        /// During SetClientSite, WMP calls this function to get the pointer to <see cref="RemoteHostInfo"/>.
        /// </summary>
        /// <param name="guidService">See MSDN for more information - we do not use this parameter.</param>
        /// <param name="riid">The Guid of the desired service to be returned.  For this application it will always match
        /// the Guid of <see cref="IWMPRemoteMediaServices"/>.</param>
        /// <returns></returns>
        IntPtr IOleServiceProvider.QueryService(ref Guid guidService, ref Guid riid) {
            //If we get to here, it means Media Player is requesting our IWMPRemoteMediaServices interface
            if(riid == new Guid("cbb92747-741f-44fe-ab5b-f1a48f3b2a59")) {
                IWMPRemoteMediaServices iwmp = new RemoteHostInfo();
                return Marshal.GetComInterfaceForObject(iwmp, typeof(IWMPRemoteMediaServices));
            }

            throw new System.Runtime.InteropServices.COMException("No Interface", (int)HResults.E_NOINTERFACE);
        }
        #endregion

        #region IOleClientSite Members
        /// <summary>
        /// Not in use.  See MSDN for details.
        /// </summary>
        /// <exception cref="System.Runtime.InteropServices.COMException">E_NOTIMPL</exception>
        void IOleClientSite.SaveObject() {
            throw new System.Runtime.InteropServices.COMException("Not Implemented", (int)HResults.E_NOTIMPL);
        }

        /// <summary>
        /// Not in use.  See MSDN for details.
        /// </summary>
        /// <exception cref="System.Runtime.InteropServices.COMException"></exception>
        object IOleClientSite.GetMoniker(uint dwAssign, uint dwWhichMoniker) {
            throw new System.Runtime.InteropServices.COMException("Not Implemented", (int)HResults.E_NOTIMPL);
        }

        /// <summary>
        /// Not in use.  See MSDN for details.
        /// </summary>
        /// <exception cref="System.Runtime.InteropServices.COMException"></exception>
        object IOleClientSite.GetContainer() {
            return (int)HResults.E_NOINTERFACE;
        }

        /// <summary>
        /// Not in use.  See MSDN for details.
        /// </summary>
        /// <exception cref="System.Runtime.InteropServices.COMException"></exception>
        void IOleClientSite.ShowObject() {
            throw new System.Runtime.InteropServices.COMException("Not Implemented", (int)HResults.E_NOTIMPL);
        }

        /// <summary>
        /// Not in use.  See MSDN for details.
        /// </summary>
        /// <exception cref="System.Runtime.InteropServices.COMException"></exception>
        void IOleClientSite.OnShowWindow(bool fShow) {
            throw new System.Runtime.InteropServices.COMException("Not Implemented", (int)HResults.E_NOTIMPL);
        }

        /// <summary>
        /// Not in use.  See MSDN for details.
        /// </summary>
        /// <exception cref="System.Runtime.InteropServices.COMException"></exception>
        void IOleClientSite.RequestNewObjectLayout() {
            throw new System.Runtime.InteropServices.COMException("Not Implemented", (int)HResults.E_NOTIMPL);
        }

        #endregion          

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public RemotedWindowsMediaPlayer() :
            base("6bf52a52-394a-11d3-b153-00c04f79faa6") {
        }
    }
}
