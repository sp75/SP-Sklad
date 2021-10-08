#include <windows.h>
#include <initguid.h>

#define NAME_SCALES_SERVER_BTA  "ScalesCOMServer.ScalesServerBTA.1"


DEFINE_GUID( IID_IScalesServerBTA,
             0x9EF1D6A6,0x0B3B,0x4979,0x8D,0x20,0x06,0xB2,0xD0,0x45,0x2B,0xF0);


enum TErrorsType
{
	errorOK						= 0,
	errorCOMPort				= 1,
	errorCOMPortIsClosed		= 2,
	errorNotResponce			= 3
};


interface IScalesServerBTA : public IDispatch
{
public:
    virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE OpenComPort( 
        /* [in] */ BSTR p_ComPortName,
        /* [out] */ LONG *p_error) = 0;
    
    virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE CloseComPort( 
        /* [out] */ LONG *p_error) = 0;
    
    virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE SetPrice( 
        /* [in] */ LONG p_Price,
        /* [out] */ LONG *p_error) = 0;
    
    virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE GetInfo( 
        /* [out] */ LONG *p_weight,
        /* [out] */ LONG *p_price,
        /* [out] */ LONG *p_cost,
        /* [out] */ LONG *p_error) = 0;
    
    virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE pressT( 
        /* [out] */ LONG *p_error) = 0;    
};
