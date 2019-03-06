using System;
using System.Runtime.InteropServices;
using System.Text;

namespace VA
{

    public class VAVec3
    {
        public double x;
        public double y;
        public double z;

        public VAVec3(double x_, double y_, double z_)
        {
            x = x_;
            y = y_;
            z = z_;
        }
    }

    public class VAQuat
    {
        public double x;
        public double y;
        public double z;
        public double w;

        public VAQuat(double x_, double y_, double z_, double w_)
        {
            x = x_;
            y = y_;
            z = z_;
            w = w_;

        }
    }


    //! Play-it-safe VA networked interface class
    /*
     * Use this class to communicate with a VA server. All server-side exceptions or not-connected exceptions
     * will be swallowed to keep the C# applications running and prevent a complete crash in native code execution.
     */
    public class VANet
    {
        private IntPtr _NetClient;

        public VANet()
        {
            _NetClient = NativeCreateNetClient();
        }

        ~VANet()
        {
            NativeDisposeNetClient(_NetClient);
        }

        public bool Connect()
        {
            return NativeConnectLocalNetClient(_NetClient);
        }

        public bool Connect(string HostIP, int Port)
        {
            return NativeConnectNetClient(_NetClient, HostIP, Port);
        }

        public bool IsConnected()
        {
            return NativeGetNetClientConnected(_NetClient);
        }

        public void Disconnect()
        {
            NativeDisconnectNetClient(_NetClient);
        }

        public void Reset()
        {
            NativeReset(_NetClient);
        }

        public bool AddSearchPath(string SearchPath)
        {
            return NativeAddSearchPath(_NetClient, SearchPath);
        }


        // Sound receiver

        // Create sound receiver
        /** 
         * Create a sound receiver (returns ID)
         */
        public int CreateSoundReceiver(string Name = "")
        {
            return NativeCreateSoundReceiver(_NetClient, Name);
        }		
		
        public int CreateSoundReceiverExplicitRenderer(string RendererID, string Name = "")
        {
            return NativeCreateSoundReceiverExplicitRenderer(_NetClient, RendererID, Name);
        }
		
        public void SetActiveSoundReceiverExplicitRenderer(string RendererID, int SoundReceiverID)
        {
            NativeSetActiveSoundReceiverExplicitRenderer(_NetClient, RendererID, SoundReceiverID);
        }
		
        public int GetActiveSoundReceiverExplicitRenderer(string RendererID)
        {
            return NativeGetActiveSoundReceiverExplicitRenderer(_NetClient, RendererID);
        }

        public void SetSoundReceiverPose(int SoundReceiver, VAVec3 v, VAQuat o)
        {
            NativeSetSoundReceiverPose(_NetClient, SoundReceiver, v.x, v.y, v.z, o.x, o.y, o.z, o.w);
		}

		public VAVec3 GetSoundReceiverRealWorldPosition(int SoundReceiver)
		{
			VAVec3 p = new VAVec3(0,0,0);
			VAQuat q = new VAQuat(0,0,0,1);
			NativeGetSoundReceiverRealWorldPose(_NetClient, SoundReceiver, ref p.x, ref p.y, ref p.z, ref q.x, ref q.y, ref q.z, ref q.w);
			return p;
		}

		public VAQuat GetSoundReceiverRealWorldOrientation(int SoundReceiver)
		{
			VAVec3 p = new VAVec3(0,0,0);
			VAQuat q = new VAQuat(0,0,0,1);
			NativeGetSoundReceiverRealWorldPose(_NetClient, SoundReceiver, ref p.x, ref p.y, ref p.z, ref q.x, ref q.y, ref q.z, ref q.w);
			return q;
		}

		public void SetSoundReceiverRealWorldHeadPose(int SoundReceiver, VAVec3 v, VAQuat o)
		{
			NativeSetSoundReceiverRealWorldPose(_NetClient, SoundReceiver, v.x, v.y, v.z, o.x, o.y, o.z, o.w);
		}

        public void SetSoundReceiverPosition(int SoundReceiver, VAVec3 v3Pos)
        {
			NativeSetSoundReceiverPosition(_NetClient, SoundReceiver, v3Pos.x, v3Pos.y, v3Pos.z);
        }

        public void SetSoundReceiverOrientation(int iSoundReceiverID, VAQuat o)
        {
            NativeSetSoundReceiverOrientation(_NetClient, iSoundReceiverID, o.x, o.y, o.z, o.w);
        }

        public void SetSoundReceiverOrientationVU(int iSoundReceiverID, VAVec3 v, VAVec3 u)
        {
            NativeSetSoundReceiverOrientationVU(_NetClient, iSoundReceiverID, v.x, v.y, v.z, u.x, u.y, u.z);
        }

        public int DeleteSoundReceiver(int iSoundReceiverID)
        {
            return NativeDeleteSoundReceiver(_NetClient, iSoundReceiverID);
        }


        // Sound sources

        // Create sound source
        /** 
         * Create a sound source (returns ID)
         */
        public int CreateSoundSource(string Name = "")
        {
            return NativeCreateSoundSource(_NetClient, Name);
        }
		
        // Create sound source explicitly for one renderer
        /** 
         * Create a sound source (returns ID) for renderer
         */
        public int CreateSoundSourceExplicitRenderer(string RendererID, string Name = "")
        {
            return NativeCreateSoundSourceExplicitRenderer(_NetClient, RendererID, Name);
        }

        public void SetSoundSourcePose(int SoundSource, VAVec3 v, VAQuat o)
        {
            NativeSetSoundSourcePose(_NetClient, SoundSource, v.x, v.y, v.z, o.x, o.y, o.z, o.w);
        }

        public void SetSoundSourcePosition(int SoundSource, VAVec3 v3Pos)
        {
            NativeSetSoundSourcePosition(_NetClient, SoundSource, v3Pos.x, v3Pos.y, v3Pos.z);
        }

        public void SetSoundSourceOrientation(int iSoundSourceID, VAQuat o)
        {
            NativeSetSoundSourceOrientation(_NetClient, iSoundSourceID, o.x, o.y, o.z, o.w);
        }

        public void SetSoundSourceOrientationVU(int iID, VAVec3 v, VAVec3 u)
        {
            NativeSetSoundSourceOrientationVU(_NetClient, iID, v.x, v.y, v.z, u.x, u.y, u.z);
        }

        public int DeleteSoundSource(int iSoundSourceID)
        {
            return NativeDeleteSoundSource(_NetClient, iSoundSourceID);
        }


        // Signal sources

        public void SetSignalSourceBufferPlaybackAction(string sSignalSourceID, string sPlaybackAction)
        {
            NativeSetSignalSourceBufferPlaybackAction(_NetClient, sSignalSourceID, sPlaybackAction);
        }
        public void SetSignalSourceBufferPlaybackPosition(string sSignalSourceID, double dPlaybackPosition)
        {
            NativeSetSignalSourceBufferPlaybackPosition(_NetClient, sSignalSourceID, dPlaybackPosition);
        }
        public bool GetSignalSourceBufferLooping(string sSignalSourceID)
        {
            return NativeGetSignalSourceBufferLooping(_NetClient, sSignalSourceID);
        }
        public void SetSignalSourceBufferLooping(string sSignalSourceID, bool bLooping)
        {
            NativeSetSignalSourceBufferLooping(_NetClient, sSignalSourceID, bLooping);
        }
        public string GetSoundSourceSignalSource(int iSoundSourceID)
        {
            StringBuilder sSignalSourceID = new StringBuilder(10 * 256);
            NativeGetSoundSourceSignalSource(_NetClient, iSoundSourceID, sSignalSourceID);
            return sSignalSourceID.ToString();
        }
        public void SetSoundSourceSignalSource(int iSoundSourceID, string sSignalSourceID)
        {
            NativeSetSoundSourceSignalSource(_NetClient, iSoundSourceID, sSignalSourceID);
        }


        public bool DeleteSignalSource(string sIdentifier)
        {
            return NativeDeleteSignalSource(_NetClient, sIdentifier);
        }

        public string CreateSignalSourceBufferFromFile(string FilePath, string sName = "")
        {
            StringBuilder sIdentifier = new StringBuilder(10 * 256);
            NativeCreateSignalSourceBufferFromFile(_NetClient, FilePath, sName, sIdentifier);
            return sIdentifier.ToString();
        }
        public string CreateSignalSourceTextToSpeech(string Name = "")
        {
            StringBuilder sIdentifier = new StringBuilder(10 * 256);
            NativeCreateSignalSourceTextToSpeech(_NetClient, Name, sIdentifier);
            return sIdentifier.ToString();
        }
        public string CreateSignalSourceEngineSignalSource(string Name = "")
        {
            StringBuilder sIdentifier = new StringBuilder(10 * 256);
            NativeCreateSignalSourceEngine(_NetClient, Name, sIdentifier);
            return sIdentifier.ToString();
        }
        public string CreateSignalSourceMachine(string Name = "")
        {
            StringBuilder sIdentifier = new StringBuilder(10 * 256);
            NativeCreateSignalSourceMachine(_NetClient, Name, sIdentifier);
            return sIdentifier.ToString();
        }
        public string CreateSignalSourceSequencer(string Name = "")
        {
            StringBuilder sIdentifier = new StringBuilder(10 * 256);
            NativeCreateSignalSourceSequencer(_NetClient, Name, sIdentifier);
            return sIdentifier.ToString();
        }
        public string CreateSignalSourceEngineSignalSource(string Name, string sInterface, int iPort)
        {
            StringBuilder sIdentifier = new StringBuilder(10 * 256);
            NativeCreateSignalSourceNetworkStream(_NetClient, Name, sInterface, iPort, sIdentifier);
            return sIdentifier.ToString();
        }

        public void SetSoundSourceSoundPower(int iSoundSourceID, double dPowerWatts)
        {
            NativeSetSoundSourceSoundPower(_NetClient, iSoundSourceID, dPowerWatts);
        }
        public void SetSoundSourceMuted(int iSoundSourceID, bool bMuted)
        {
            NativeSetSoundSourceMuted(_NetClient, iSoundSourceID, bMuted);
        }
        public int CreateDirectivityFromFile(string sFileName)
        {
            return NativeCreateDirectivityFromFile(_NetClient, sFileName);
        }
        public bool DeleteDirectivity(int iDirID)
        {
            return NativeDeleteDirectivity(_NetClient, iDirID);
        }
        public void SetSoundSourceEnabled(int iSoundSourceID, bool bEnabled)
        {
            NativeSetSoundSourceEnabled(_NetClient, iSoundSourceID, bEnabled);
        }

        public void SetSoundReceiverEnabled(int iSoundReceiverID, bool bEnabled)
        {
            NativeSetSoundReceiverEnabled(_NetClient, iSoundReceiverID, bEnabled);
        }
        public void SetSoundReceiverDirectivity(int iSoundReceiverID, int iDirID)
        {
            NativeSetSoundReceiverDirectivity(_NetClient, iSoundReceiverID, iDirID);
        }

        public void SetSoundSourceDirectivity(int iSoundSourceID, int iDirectivityID)
        {
            NativeSetSoundSourceDirectivity(_NetClient, iSoundSourceID, iDirectivityID);
        }

        public void SetGlobalAuralizationMode(string sAuralizationMode)
        {
            NativeSetGlobalAuralizationMode(_NetClient, sAuralizationMode);
        }

        public void SetSoundReceiverAuralizationMode(int iSoundReceiverID, string sAuralizationMode)
        {
            NativeSetSoundReceiverAuralizationMode(_NetClient, iSoundReceiverID, sAuralizationMode);
        }

        public void SetSoundSourceAuralizationMode(int iSoundSourceID, string sAuralizationMode)
        {
            NativeSetSoundSourceAuralizationMode(_NetClient, iSoundSourceID, sAuralizationMode);
        }

        public void SetArtificialReverberationTime(string Renderer, double ReverbTime)
        {
            NativeSetArtificialReverberationTime(_NetClient, Renderer, ReverbTime);
        }

        public void SetArtificialRoomVolume(string Renderer, double ReverbTime)
        {
            NativeSetArtificialRoomVolume(_NetClient, Renderer, ReverbTime);
        }

        public void SetArtificialSurfaceArea(string Renderer, double ReverbTime)
        {
            NativeSetArtificialSurfaceArea(_NetClient, Renderer, ReverbTime);
        }

        // Homogeneous medium
        public void SetHomogeneousMediumSoundSpeed(double SoundSpeed)
        {
            NativeSetHomogeneousMediumSoundSpeed(_NetClient, SoundSpeed);
        }
        public double GetHomogeneousMediumSoundSpeed()
        {
            return NativeGetHomogeneousMediumSoundSpeed(_NetClient);
        }
        public void SetHomogeneousMediumRelativeHumidity(double RelativeHumidity)
        {
            NativeSetHomogeneousMediumRelativeHumidity(_NetClient, RelativeHumidity);
        }
        public double GetHomogeneousMediumRelativeHumidity()
        {
            return NativeGetHomogeneousMediumRelativeHumidity(_NetClient);
        }
        public void SetHomogeneousMediumShiftSpeed(VAVec3 ShiftSpeed)
        {
            NativeSetHomogeneousMediumShiftSpeed(_NetClient, ShiftSpeed.x, ShiftSpeed.y, ShiftSpeed.z);
        }
        public VAVec3 GetHomogeneousMediumShiftSpeed()
        {
            double x = 0;
            double y = 0;
            double z = 0;
            NativeGetHomogeneousMediumShiftSpeed(_NetClient, ref x, ref y, ref z);
            return new VAVec3(x, y, z);
        }
        public void SetHomogeneousMediumStaticPressure(double StaticPressure)
        {
            NativeSetHomogeneousMediumStaticPressure(_NetClient, StaticPressure);
        }
        public double GetHomogeneousMediumStaticPressure()
        {
            return NativeGetHomogeneousMediumStaticPressure(_NetClient);
        }
        public void SetHomogeneousMediumTemperature(double Temperature)
        {
            NativeSetHomogeneousMediumTemperature(_NetClient, Temperature);
        }
        public double GetHomogeneousMediumTemperature()
        {
            return NativeGetHomogeneousMediumTemperature(_NetClient);
        }


        // Scenes

        public string CreateSceneFromFile(string FilePath)
        {
            StringBuilder Identifier = new StringBuilder(10 * 256);
            NativeCreateSceneFromFile(_NetClient, FilePath, Identifier);
            return Identifier.ToString();
        }
        public string GetSceneName(string ID)
        {
            StringBuilder Name = new StringBuilder(10 * 256);
            NativeGetSceneName(_NetClient, ID, Name);
            return Name.ToString();
        }
        public void SetSceneName(string ID, string Name)
        {
            NativeSetSceneName(_NetClient, ID, Name);
        }
        public bool GetSceneEnabled(string ID)
        {
            return NativeGetSceneEnabled(_NetClient, ID);
        }
        public void SetSceneEnabled(string ID, bool Enabled)
        {
            NativeSetSceneEnabled(_NetClient, ID, Enabled);
        }


        // Geometry meshes


        public int CreateGeometryMeshFromFile(string FilePath)
        {
            return NativeCreateGeometryMeshFromFile(_NetClient, FilePath);
        }
        public string GetGeometryMeshName(int ID)
        {
            StringBuilder Name = new StringBuilder(10 * 256);
            NativeGetGeometryMeshName(_NetClient, ID, Name);
            return Name.ToString();
        }
        public void SetGeometryMeshName(int ID, string Name)
        {
            NativeSetGeometryMeshName(_NetClient, ID, Name);
        }
        public bool GetGeometryMeshEnabled(int ID)
        {
            return NativeGetGeometryMeshEnabled(_NetClient, ID);
        }
        public void SetGeometryMeshEnabled(int ID, bool Enabled)
        {
            NativeSetGeometryMeshEnabled(_NetClient, ID, Enabled);
        }


        public bool GetInputMuted()
        {
            return NativeGetInputMuted(_NetClient);
        }
        public bool GetOutputMuted()
        {
            return NativeGetOutputMuted(_NetClient);
        }
        public void SetOutputMuted(bool bMuted)
        {
            NativeSetOutputMuted(_NetClient, bMuted);
        }
        public void SetInputMuted(bool bMuted)
        {
            NativeSetInputMuted(_NetClient, bMuted);
        }
        public void SetInputGain(double dGain)
        {
            NativeSetInputGain(_NetClient, dGain);
        }
        public double GetInputGain()
        {
            return NativeGetInputGain(_NetClient);
        }
        public void SetOutputGain(double dGain)
        {
            NativeSetOutputGain(_NetClient, dGain);
        }
        public double GetOutputGain()
        {
            return NativeGetOutputGain(_NetClient);
        }

        public void SetRenderingModuleMuted(string sModuleID, bool bMuted)
        {
            NativeSetRenderingModuleMuted(_NetClient, sModuleID, bMuted);
        }
        public bool GetRenderingModuleMuted(string sModuleID)
        {
            return NativeGetRenderingModuleMuted(_NetClient, sModuleID);
        }
        public double GetRenderingModuleGain(string sModuleID)
        {
            return NativeGetRenderingModuleGain(_NetClient, sModuleID);
        }
        public void SetRenderingModuleGain(string sModuleID, double dGain)
        {
            NativeSetRenderingModuleGain(_NetClient, sModuleID, dGain);
        }
        public void SetReproductionModuleMuted(string sModuleID, bool bMuted)
        {
            NativeSetReproductionModuleMuted(_NetClient, sModuleID, bMuted);
        }
        public bool GetReproductionModuleMuted(string sModuleID)
        {
            return NativeGetReproductionModuleMuted(_NetClient, sModuleID);
        }
        public double GetReproductionModuleGain(string sModuleID)
        {
            return NativeGetReproductionModuleGain(_NetClient, sModuleID);
        }
        public void SetReproductionModuleGain(string sModuleID, double dGain)
        {
            NativeSetReproductionModuleGain(_NetClient, sModuleID, dGain);
        }

        public void SetSoundReceiverAnthropometricData(int iSoundReceiverID, double dHeadWidth, double dHeadHeight, double dHeadDepth)
        {
            NativeSetSoundReceiverAnthropometricData(_NetClient, iSoundReceiverID, dHeadWidth, dHeadHeight, dHeadDepth);
        }

        public void TextToSpeechPrepareTextAndPlaySpeech(string sSignalSourceIdentifier, string sText)
        {
            NativeTextToSpeechPrepareTextAndPlaySpeech(_NetClient, sSignalSourceIdentifier, sText);
        }
        public void TextToSpeechPrepareText(string sSignalSourceIdentifier, string sTextIdentifier, string sText)
        {
            NativeTextToSpeechPrepareText(_NetClient, sSignalSourceIdentifier, sTextIdentifier, sText);
        }
        public void TextToSpeechPlaySpeech(string sSignalSourceIdentifier, string sTextIdentifier)
        {
            NativeTextToSpeechPlaySpeech(_NetClient, sSignalSourceIdentifier, sTextIdentifier);
        }

        public void UpdateGenericPath(string sModuleID, int iSource, int iReceiver, int iChannel, float fDelaySeconds, int iNumSamples, float[] vfSampleBuffer)
        {
            NativeUpdateGenericPath(_NetClient, sModuleID, iSource, iReceiver, iChannel, fDelaySeconds, iNumSamples, vfSampleBuffer);
        }
        public void UpdateGenericPathFromFile(string sModuleID, int iSource, int iReceiver, string sFilePath)
        {
            NativeUpdateGenericPathFromFile(_NetClient, sModuleID, iSource, iReceiver, sFilePath);
        }


        /*
         * Native imported functions from C++ unmanaged library declared private, so they can not be accessed
         * directly through C# class method
         * 
         */


        // Communication

        [DllImport("VANetCSWrapper")]
        private static extern IntPtr NativeCreateNetClient();
        [DllImport("VANetCSWrapper")]
        private static extern bool NativeConnectLocalNetClient(IntPtr pClient);
        [DllImport("VANetCSWrapper")]
        private static extern bool NativeConnectNetClient(IntPtr pClient, string sServerIP, int iPort);
        [DllImport("VANetCSWrapper")]
        private static extern bool NativeGetNetClientConnected(IntPtr pClient);
        [DllImport("VANetCSWrapper")]
        private static extern bool NativeDisconnectNetClient(IntPtr pClient);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeDisposeNetClient(IntPtr pClient);
        [DllImport("VANetCSWrapper")]
        private static extern int NativeGetState(IntPtr pClient);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeInitialize(IntPtr pClient);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeFinalize(IntPtr pClient);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeReset(IntPtr pClient);
        [DllImport("VANetCSWrapper")]
        private static extern bool NativeAddSearchPath(IntPtr pClient, string sSearchPath);


        // Directivities

        [DllImport("VANetCSWrapper")]
        private static extern int NativeCreateDirectivityFromFile(IntPtr pClient, string sFilePath);
        [DllImport("VANetCSWrapper")]
        private static extern bool NativeDeleteDirectivity(IntPtr pClient, int iDirID);


        // Signal sources

        [DllImport("VANetCSWrapper")]
        private static extern int NativeCreateSignalSourceBufferFromFile(IntPtr pClient, string sFileName, string sName, StringBuilder sIdentifier);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeCreateSignalSourceSequencer(IntPtr pClient, string sName, StringBuilder sIdentifier);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeCreateSignalSourceNetworkStream(IntPtr pClient, string sName, string sInterface, int iPort, StringBuilder sIdentifier);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeCreateSignalSourceEngine(IntPtr pClient, string sName, StringBuilder sIdentifier);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeCreateSignalSourceMachine(IntPtr pClient, string sName, StringBuilder sIdentifier);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeCreateSignalSourceTextToSpeech(IntPtr pClient, string sName, StringBuilder sSignalSourceIdentifier);
        [DllImport("VANetCSWrapper")]
        private static extern bool NativeDeleteSignalSource(IntPtr pClient, string sIdentifier);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeGetSignalSourceBufferPlaybackState(IntPtr pClient, string sSignalSourceID, string sPlaybackState);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetSignalSourceBufferPlaybackAction(IntPtr pClient, string sSignalSourceID, string sPlaybackAction);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetSignalSourceBufferPlaybackPosition(IntPtr pClient, string sSignalSourceID, double dPlaybackPosition);
        [DllImport("VANetCSWrapper")]
        private static extern bool NativeGetSignalSourceBufferLooping(IntPtr pClient, string sSignalSourceID);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetSignalSourceBufferLooping(IntPtr pClient, string sSignalSourceID, bool bLooping);


        // Update sync

        [DllImport("VANetCSWrapper")]
        private static extern bool NativeGetUpdateLocked(IntPtr pClient);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeLockUpdate(IntPtr pClient);
        [DllImport("VANetCSWrapper")]
        private static extern int NativeUnlockScene(IntPtr pClient);


        // Sound sources

        [DllImport("VANetCSWrapper")]
        private static extern int NativeCreateSoundSource(IntPtr pClient, string sName);
        [DllImport("VANetCSWrapper")]
        private static extern int NativeCreateSoundSourceExplicitRenderer(IntPtr pClient, string sRendererID, string sName);
        [DllImport("VANetCSWrapper")]
        private static extern int NativeDeleteSoundSource(IntPtr pClient, int iSoundSourceID);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetSoundSourceEnabled(IntPtr pClient, int iSoundSourceID, bool bEnabled);
        [DllImport("VANetCSWrapper")]
        private static extern bool NativeGetSoundSourceEnabled(IntPtr pClient, int iSoundSourceID);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeGetSoundSourceName(IntPtr pClient, int iSoundSourceID, string sName);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetSoundSourceName(IntPtr pClient, int iSoundSourceID, string sName);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeGetSoundSourceSignalSource(IntPtr pClient, int iSoundSourceID, StringBuilder sSignalSourceID);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetSoundSourceSignalSource(IntPtr pClient, int iSoundSourceID, string sSignalSourceID);
        [DllImport("VANetCSWrapper")]
        private static extern int NativeGetSoundSourceAuralizationMode(IntPtr pClient, int iSoundSourceID);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetSoundSourceAuralizationMode(IntPtr pClient, int iSoundSourceID, string iAuralizationMode);
        [DllImport("VANetCSWrapper")]
        private static extern int NativeGetSoundSourceDirectivity(IntPtr pClient, int iSoundSourceID);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetSoundSourceDirectivity(IntPtr pClient, int iSoundSourceID, int iDirectivityID);
        [DllImport("VANetCSWrapper")]
        private static extern double NativeGetSoundSourceSoundPower(IntPtr pClient, int iSoundSourceID);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetSoundSourceSoundPower(IntPtr pClient, int iSoundSourceID, double dSoundPowerWatts);
        [DllImport("VANetCSWrapper")]
        private static extern bool NativeGetSoundSourceMuted(IntPtr pClient, int iSoundSourceID);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetSoundSourceMuted(IntPtr pClient, int iSoundSourceID, bool bMuted);

        [DllImport("VANetCSWrapper")]
        private static extern void NativeGetSoundSourcePose(IntPtr pClient, int iID, ref double x, ref double y, ref double z, ref double ox, ref double oy, ref double oz, ref double ow);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetSoundSourcePose(IntPtr pClient, int iID, double x, double y, double z, double ox, double oy, double oz, double ow);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeGetSoundSourcePosition(IntPtr pClient, int iID, ref double x, ref double y, ref double z);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetSoundSourcePosition(IntPtr pClient, int iID, double x, double y, double z);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeGetSoundSourceOrientation(IntPtr pClient, int iID, ref double ox, ref double oy, ref double oz, ref double ow);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetSoundSourceOrientation(IntPtr pClient, int iID, double ox, double oy, double oz, double ow);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeGetSoundSourceOrientation(IntPtr pClient, int iID, double x, double y, double z, double w);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeGetSoundSourceOrientationVU(IntPtr pClient, int iID, ref double vx, ref double vy, ref double vz, ref double ux, ref double uy, ref double uz);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetSoundSourceOrientationVU(IntPtr pClient, int iID, double vx, double vy, double vz, double ux, double uy, double uz);


        // Sound receivers

        [DllImport("VANetCSWrapper")]
        private static extern int NativeCreateSoundReceiver(IntPtr pClient, string sName);
        [DllImport("VANetCSWrapper")]
        private static extern int NativeCreateSoundReceiverExplicitRenderer(IntPtr pClient, string sRendererID, string sName);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetActiveSoundReceiverExplicitRenderer(IntPtr pClient, string sRendererID, int iSoundReceiverID);
        [DllImport("VANetCSWrapper")]
        private static extern int NativeGetActiveSoundReceiverExplicitRenderer(IntPtr pClient, string sRendererID);
        [DllImport("VANetCSWrapper")]
        private static extern int NativeDeleteSoundReceiver(IntPtr pClient, int iSoundReceiverID);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetSoundReceiverEnabled(IntPtr pClient, int iSoundReceiverID, bool bEnabled);
        [DllImport("VANetCSWrapper")]
        private static extern bool NativeGetSoundReceiverEnabled(IntPtr pClient, int iSoundReceiverID);
        [DllImport("VANetCSWrapper")]
        private static extern void GetSoundReceiverName(IntPtr pClient, int iSoundReceiverID, char cName);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetSoundReceiverName(IntPtr pClient, int iSoundReceiverID, string sName);
        [DllImport("VANetCSWrapper")]
        private static extern int NativeGetSoundReceiverAuralizationMode(IntPtr pClient, int iSoundReceiverID);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetSoundReceiverAuralizationMode(IntPtr pClient, int iSoundReceiverID, string sAuralizationMode);
        [DllImport("VANetCSWrapper")]
        private static extern int NativeGetSoundReceiverDirectivity(IntPtr pClient, int iSoundReceiverID);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetSoundReceiverDirectivity(IntPtr pClient, int iSoundReceiverID, int iDirID);

        [DllImport("VANetCSWrapper")]
        private static extern void NativeGetSoundReceiverPose(IntPtr pClient, int iSoundReceiverID, ref double x, ref double y, ref double z, ref double ox, ref double oy, ref double oz, ref double ow);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetSoundReceiverPose(IntPtr pClient, int iSoundReceiverID, double x, double y, double z, double ox, double oy, double oz, double ow);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeGetSoundReceiverPosition(IntPtr pClient, int iSoundReceiverID, ref double x, ref double y, ref double z);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetSoundReceiverPosition(IntPtr pClient, int iSoundReceiverID, double x, double y, double z);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeGetSoundReceiverOrientation(IntPtr pClient, int iSoundReceiverID, ref double x, ref double y, ref double z, ref double w);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetSoundReceiverOrientation(IntPtr pClient, int iSoundReceiverID, double qx, double qy, double qz, double qw);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeGetSoundReceiverOrientationVU(IntPtr pClient, int iSoundReceiverID, ref double vx, ref double vy, ref double vz, ref double ux, ref double uy, ref double uz);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetSoundReceiverOrientationVU(IntPtr pClient, int iSoundReceiverID, double vx, double vy, double vz, double ux, double uy, double uz);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeGetSoundReceiverHeadAboveTorsoOrientation(IntPtr pClient, int iSoundReceiverID, ref double qx, ref double qy, ref double qz, ref double qw);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetSoundReceiverHeadAboveTorsoOrientation(IntPtr pClient, int iSoundReceiverID, double qx, double qy, double qz, double qw);


        [DllImport("VANetCSWrapper")]
        private static extern void NativeGetSoundReceiverRealWorldPositionOrientationVU(IntPtr pClient, int iSoundReceiverID, ref double px, ref double py, ref double pz, ref double vx, ref double vy, ref double vz, ref double ux, ref double uy, ref double uz);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetSoundReceiverRealWorldPositionOrientationVU(IntPtr pClient, int iSoundReceiverID, double px, double py, double pz, double vx, double vy, double vz, double ux, double uy, double uz);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeGetSoundReceiverRealWorldPose(IntPtr pClient, int iSoundReceiverID, ref double px, ref double py, ref double pz, ref double x, ref double y, ref double z, ref double w);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetSoundReceiverRealWorldPose(IntPtr pClient, int iSoundReceiverID, double px, double py, double pz, double qx, double qy, double qz, double qw);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeGetSoundReceiverRealWorldHeadAboveTorsoOrientation(IntPtr pClient, int iSoundReceiverID, ref double qx, ref double qy, ref double qz, ref double qw);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetSoundReceiverRealWorldHeadAboveTorsoOrientation(IntPtr pClient, int iSoundReceiverID, double qx, double qy, double qz, double qw);


        // Homogeneous medium
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetHomogeneousMediumSoundSpeed(IntPtr pClient, double dSoundSpeed);
        [DllImport("VANetCSWrapper")]
        private static extern double NativeGetHomogeneousMediumSoundSpeed(IntPtr pClient);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetHomogeneousMediumTemperature(IntPtr pClient, double dDegreesCentigrade);
        [DllImport("VANetCSWrapper")]
        private static extern double NativeGetHomogeneousMediumTemperature(IntPtr pClient);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetHomogeneousMediumStaticPressure(IntPtr pClient, double dPressurePascal);
        [DllImport("VANetCSWrapper")]
        private static extern double NativeGetHomogeneousMediumStaticPressure(IntPtr pClient);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetHomogeneousMediumRelativeHumidity(IntPtr pClient, double dRelativeHumidityPercent);
        [DllImport("VANetCSWrapper")]
        private static extern double NativeGetHomogeneousMediumRelativeHumidity(IntPtr pClient);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetHomogeneousMediumShiftSpeed(IntPtr pClient, double x, double y, double z);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeGetHomogeneousMediumShiftSpeed(IntPtr pClient, ref double x, ref double y, ref double z);


        // Scenes

        [DllImport("VANetCSWrapper")]
        private static extern void NativeCreateSceneFromFile(IntPtr pClient, string sFilePath, StringBuilder sSceneID);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeGetSceneName(IntPtr pClient, string sID, StringBuilder sName);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetSceneName(IntPtr pClient, string sID, string sName);
        [DllImport("VANetCSWrapper")]
        private static extern bool NativeGetSceneEnabled(IntPtr pClient, string sID);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetSceneEnabled(IntPtr pClient, string sID, bool bEnabled);


        // Geometry meshes

        [DllImport("VANetCSWrapper")]
        private static extern int NativeCreateGeometryMeshFromFile(IntPtr pClient, string sFilePath);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeDeleteGeometryMesh(IntPtr pClient, int iID);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeGetGeometryMeshName(IntPtr pClient, int iID, StringBuilder sName);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetGeometryMeshName(IntPtr pClient, int iID, string sName);
        [DllImport("VANetCSWrapper")]
        private static extern bool NativeGetGeometryMeshEnabled(IntPtr pClient, int iID);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetGeometryMeshEnabled(IntPtr pClient, int iID, bool bEnabled);


        // Portals

        // @todo jst mim ahe


        // Global control

        [DllImport("VANetCSWrapper")]
        private static extern bool NativeGetInputMuted(IntPtr pClient);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetInputMuted(IntPtr pClient, bool bMuted);
        [DllImport("VANetCSWrapper")]
        private static extern double NativeGetInputGain(IntPtr pClient);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetInputGain(IntPtr pClient, double dGain);
        [DllImport("VANetCSWrapper")]
        private static extern double NativeGetOutputGain(IntPtr pClient);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetOutputGain(IntPtr pClient, double dGain);
        [DllImport("VANetCSWrapper")]
        private static extern bool NativeGetOutputMuted(IntPtr pClient);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetOutputMuted(IntPtr pClient, bool bMuted);

        [DllImport("VANetCSWrapper")]
        private static extern int NativeGetGlobalAuralizationMode(IntPtr pClient);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetGlobalAuralizationMode(IntPtr pClient, string sAuralizationMode);


        [DllImport("VANetCSWrapper")]
        private static extern int NativeGetActiveSoundReceiver(IntPtr pClient);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetActiveSoundReceiver(IntPtr pClient, int iSoundReceiverID);


        [DllImport("VANetCSWrapper")]
        private static extern double NativeGetCoreClock(IntPtr pClient);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetCoreClock(IntPtr pClient, double dSeconds);


        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetRenderingModuleMuted(IntPtr pClient, string sModuleID, bool bMuted);
        [DllImport("VANetCSWrapper")]
        private static extern bool NativeGetRenderingModuleMuted(IntPtr pClient, string sModuleID);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetRenderingModuleGain(IntPtr pClient, string sModuleID, double dGain);
        [DllImport("VANetCSWrapper")]
        private static extern double NativeGetRenderingModuleGain(IntPtr pClient, string sModuleID);


        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetReproductionModuleMuted(IntPtr pClient, string sModuleID, bool bMuted);
        [DllImport("VANetCSWrapper")]
        private static extern bool NativeGetReproductionModuleMuted(IntPtr pClient, string sModuleID);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetReproductionModuleGain(IntPtr pClient, string sModuleID, double dGain);
        [DllImport("VANetCSWrapper")]
        private static extern double NativeGetReproductionModuleGain(IntPtr pClient, string sModuleID);


        // Special methods that are not part of the VA interface, but used over magic parameter setter/getter

        // Binaural artificial reverberation

        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetArtificialReverberationTime(IntPtr pClient, string sRendererID, double dReverberationTime);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetArtificialRoomVolume(IntPtr pClient, string sRendererID, double dVolume);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetArtificialSurfaceArea(IntPtr pClient, string sRendererID, double dArea);

        // Anthropometric data setter for individualized SoundReceiver rendering

        [DllImport("VANetCSWrapper")]
        private static extern void NativeSetSoundReceiverAnthropometricData(IntPtr pClient, int iSoundReceiverID, double dHeadWidth, double dHeadHeight, double dHeadDepth);

        // Text to speech signal source

        [DllImport("VANetCSWrapper")]
        private static extern void NativeTextToSpeechPrepareTextAndPlaySpeech(IntPtr pClient, string sSignalSourceIdentifier, string sText);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeTextToSpeechPrepareText(IntPtr pClient, string sSignalSourceIdentifier, string sTextIdentifier, string sText);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeTextToSpeechPlaySpeech(IntPtr pClient, string sSignalSourceIdentifier, string sTextIdentifier);

        // Generic path renderer

        [DllImport("VANetCSWrapper")]
        private static extern void NativeUpdateGenericPath(IntPtr pClient, string sRendererID, int iSourceID, int iReceiverID, int iChannel, float fDelaySeconds, int iNumSamples, [In] float[] vfSampleBuffer);
        [DllImport("VANetCSWrapper")]
        private static extern void NativeUpdateGenericPathFromFile(IntPtr pClient, string sRendererID, int iSourceID, int iReceiverID, string sFilePath);

        // Ambient mixer renderer
        
        [DllImport("VANetCSWrapper")]
        private static extern void NativeAmbientMixerRendererPlaySampleFromFile( IntPtr pClient, string sRendererID, string sSampleFilePath );

    }
}

