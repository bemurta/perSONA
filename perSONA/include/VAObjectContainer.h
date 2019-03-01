/*
 *  --------------------------------------------------------------------------------------------
 *
 *    VVV        VVV A           Virtual Acoustics (VA) | http://www.virtualacoustics.org
 *     VVV      VVV AAA          Licensed under the Apache License, Version 2.0
 *      VVV    VVV   AAA
 *       VVV  VVV     AAA        Copyright 2015-2018
 *        VVVVVV       AAA       Institute of Technical Acoustics (ITA)
 *         VVVV         AAA      RWTH Aachen University
 *
 *  --------------------------------------------------------------------------------------------
 */

#ifndef IW_VACORE_OBJECTCONTAINER
#define IW_VACORE_OBJECTCONTAINER

#include <limits>
#include <map>
#include <set>
#include <cstdint>
#include <climits>

/**
 * Diese generische Klasse realisiert Container f�r Objekte in VA (Schallquellen, H�rer, ...)
 * welche �ber Identifikationsnummern (IDs) indiziert werden. Die Klasse verwaltet die IDs selbst und
 * assoziert diese mit einem Objekt des parametrisierten Typs und umgekehrt. Beide
 * Entit�ten stehen dabei in einer 1:1-Beziehung. Der Container enth�lt keine doppelten Objekte.
 * Der Datentyp f�r ID ist immer Integer mit Vorzeichen.
 * Zus�tzlich verwaltet die Klasse f�r jedes enthaltene Objekt einen Referenzz�hler.
 *
 * Wichtig: Die Klasse ist nicht Thread-safe! Sie muss von aussen abgesichert werden.
 */

template< typename T > class CVAObjectContainer
{
public:
	// iEndID letztm�gliche ID. Falls �berlauf -> Exception
	inline CVAObjectContainer( int iStartID = 1, int iLastID = std::numeric_limits<std::int32_t>::max() )
		: m_iStartID( iStartID )
		, m_iIDCount( iStartID )
		, m_iIDLast( iLastID )
	{
	};

	inline virtual ~CVAObjectContainer()
	{
	};

	// L�scht alle Elemente im Container
	inline void Clear()
	{
		m_mID2Object.clear();
		m_mObject2ID.clear();
		m_mID2Ref.clear();
		m_iIDCount = m_iStartID;
	};

	// F�gt dem Container ein neues Objekt hinzu und gibt die ID zur�ck
	// (Falls das Objekt schon enthalten war, wird die bereits zugeordnete ID zur�ckgegeben)
	inline int Add( T* pObject )
	{
		// Zun�chst pr�fen, ob das Objekt schon enthalten ist
		int iID = GetID( pObject );
		if( iID != -1 )
			return iID;

		// Freie ID holen
		iID = m_iIDCount++;

		// Zuordnungen setzen
		//m_sElements.insert(pObject);
		m_mID2Object.insert( std::pair< int, T* >( iID, pObject ) );
		m_mObject2ID.insert( std::pair< T*, int >( pObject, iID ) );

		// Referenzz�hler initialisieren
		m_mID2Ref.insert( std::pair< int, int >( iID, 0 ) );

		return iID;
	};

	inline void Remove( int iID )
	{
		typename std::map< int, T* >::iterator it = m_mID2Object.find( iID );

		if( it == m_mID2Object.end() )
			return;

		T* pObject = it->second;
		m_mObject2ID.erase( pObject );
		m_mID2Object.erase( iID );
		m_mID2Ref.erase( iID );
	};

	// Entfernt ein enthaltenes Objekt
	inline void Remove( T* pObject )
	{
		typename std::map< T*, int >::iterator it = m_mObject2ID.find( pObject );

		// Nicht enthalten
		if( it == m_mObject2ID.end() ) return;

		int iID = it->second;
		m_mID2Object.erase( iID );
		m_mObject2ID.erase( pObject );
		m_mID2Ref.erase( iID );
	};

	//! Gibt zu einer ID das assozierte Objekt zur�ck
	/** (Gibt nullptr zur�ck, falls das Objekt nicht enthalten ist)
	  */
	inline T* GetObject( const int iID ) const
	{
		typename std::map< int, T* >::const_iterator cit = m_mID2Object.find( iID );
		return ( cit == m_mID2Object.end() ? nullptr : cit->second );
	};

	// Gibt zu einem Objekt die zugeordnete ID zur�ck
	// (Gibt -1 zur�ck, falls Objekt nicht enthalten)
	inline int GetID( T* pObject ) const
	{
		typename std::map<T*, int>::const_iterator cit = m_mObject2ID.find( pObject );
		return ( cit == m_mObject2ID.end() ? -1 : cit->second );
	};

	// Referenzz�hler eines Objektes zur�ckgeben (R�ckgabe -1 falls ID ung�ltig)
	inline int GetRefCount( int iID ) const
	{
		typename std::map<int, int>::const_iterator cit = m_mID2Ref.find( iID );
		return ( cit == m_mID2Ref.end() ? -1 : cit->second );
	};

	// Referenzz�hler eines Objektes inkrementieren (gibt Wert nach Inkrementierung zur�ck, R�ckgabe -1 falls ID ung�ltig)
	inline int IncRefCount( int iID )
	{
		typename std::map<int, int>::iterator it = m_mID2Ref.find( iID );
		return ( it == m_mID2Ref.end() ? -1 : ++( it->second ) );
	};

	// Referenzz�hler eines Objektes dekrementieren (gibt Wert nach Dekrementierung zur�ck, R�ckgabe -1 falls ID ung�ltig)
	inline int DecRefCount( int iID )
	{
		typename std::map<int, int>::iterator it = m_mID2Ref.find( iID );
		return ( it == m_mID2Ref.end() ? -1 : ( it->second > 0 ? --( it->second ) : 0 ) );
	};

	//! Wie GetObject(), erh�ht aber direkt den Referenzz�hler
	inline T* Request( const int iID )
	{
		T* pObject = GetObject( iID );

		// G�ltige ID? Dann Referenzz�hler erh�hen
		if( pObject )
			IncRefCount( iID );

		return pObject;
	};

	// Alias f�r DecRefCount() (R�ckgabewert: Verbleibende Anzahl Referenzen, R�ckgabe -1 falls ID ung�ltig)
	inline int Release( int iID )
	{
		return DecRefCount( iID );
	};

	// Der Operator [] ist Alias f�r GetObject()
	inline T* operator[]( int iID ) const
	{
		return GetObject( iID );
	};

	// --= Realisierung des STL-Iterator-Patterns =--

	/*
	 *  Hinweis: Der Iterator arbeitet wie bei std::map �ber Paare (std::pair) von <int, T*>
	 */

	typedef typename std::map<int, T*>::iterator iterator;
	typedef typename std::map<int, T*>::const_iterator const_iterator;

	inline iterator begin()
	{ 
		return m_mID2Object.begin();
	};

	inline iterator end()
	{
		return m_mID2Object.end();
	};

	inline const_iterator begin() const 
	{ 
		return m_mID2Object.begin();
	};

	inline const_iterator end() const
	{
		return m_mID2Object.end();
	};

	inline T& front() 
	{ 
		return m_mID2Object.front();
	};

	inline T& back() 
	{ 
		return m_mID2Object.back(); 
	};

	inline const T& front() const 
	{
		return m_mID2Object.front();
	};

	inline const T& back() const 
	{
		return m_mID2Object.back();
	};

	inline size_t size() const 
	{
		return m_mID2Object.size();
	};

	inline bool empty() const 
	{
		return m_mID2Object.empty();
	};

private:
	int m_iStartID, m_iIDCount, m_iIDLast;
	std::map< int, T* > m_mID2Object;	// Assoziationen ID -> Objekt
	std::map< T*, int > m_mObject2ID;	// Assoziationen Objekt -> ID
	std::map< int, int > m_mID2Ref;	// Referenzz�hler
};

#endif // IW_VACORE_OBJECTCONTAINER
