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
 * Diese generische Klasse realisiert Container für Objekte in VA (Schallquellen, Hörer, ...)
 * welche über Identifikationsnummern (IDs) indiziert werden. Die Klasse verwaltet die IDs selbst und
 * assoziert diese mit einem Objekt des parametrisierten Typs und umgekehrt. Beide
 * Entitäten stehen dabei in einer 1:1-Beziehung. Der Container enthält keine doppelten Objekte.
 * Der Datentyp für ID ist immer Integer mit Vorzeichen.
 * Zusätzlich verwaltet die Klasse für jedes enthaltene Objekt einen Referenzzähler.
 *
 * Wichtig: Die Klasse ist nicht Thread-safe! Sie muss von aussen abgesichert werden.
 */

template< typename T > class CVAObjectContainer
{
public:
	// iEndID letztmögliche ID. Falls Überlauf -> Exception
	inline CVAObjectContainer( int iStartID = 1, int iLastID = std::numeric_limits<std::int32_t>::max() )
		: m_iStartID( iStartID )
		, m_iIDCount( iStartID )
		, m_iIDLast( iLastID )
	{
	};

	inline virtual ~CVAObjectContainer()
	{
	};

	// Löscht alle Elemente im Container
	inline void Clear()
	{
		m_mID2Object.clear();
		m_mObject2ID.clear();
		m_mID2Ref.clear();
		m_iIDCount = m_iStartID;
	};

	// Fügt dem Container ein neues Objekt hinzu und gibt die ID zurück
	// (Falls das Objekt schon enthalten war, wird die bereits zugeordnete ID zurückgegeben)
	inline int Add( T* pObject )
	{
		// Zunächst prüfen, ob das Objekt schon enthalten ist
		int iID = GetID( pObject );
		if( iID != -1 )
			return iID;

		// Freie ID holen
		iID = m_iIDCount++;

		// Zuordnungen setzen
		//m_sElements.insert(pObject);
		m_mID2Object.insert( std::pair< int, T* >( iID, pObject ) );
		m_mObject2ID.insert( std::pair< T*, int >( pObject, iID ) );

		// Referenzzähler initialisieren
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

	//! Gibt zu einer ID das assozierte Objekt zurück
	/** (Gibt nullptr zurück, falls das Objekt nicht enthalten ist)
	  */
	inline T* GetObject( const int iID ) const
	{
		typename std::map< int, T* >::const_iterator cit = m_mID2Object.find( iID );
		return ( cit == m_mID2Object.end() ? nullptr : cit->second );
	};

	// Gibt zu einem Objekt die zugeordnete ID zurück
	// (Gibt -1 zurück, falls Objekt nicht enthalten)
	inline int GetID( T* pObject ) const
	{
		typename std::map<T*, int>::const_iterator cit = m_mObject2ID.find( pObject );
		return ( cit == m_mObject2ID.end() ? -1 : cit->second );
	};

	// Referenzzähler eines Objektes zurückgeben (Rückgabe -1 falls ID ungültig)
	inline int GetRefCount( int iID ) const
	{
		typename std::map<int, int>::const_iterator cit = m_mID2Ref.find( iID );
		return ( cit == m_mID2Ref.end() ? -1 : cit->second );
	};

	// Referenzzähler eines Objektes inkrementieren (gibt Wert nach Inkrementierung zurück, Rückgabe -1 falls ID ungültig)
	inline int IncRefCount( int iID )
	{
		typename std::map<int, int>::iterator it = m_mID2Ref.find( iID );
		return ( it == m_mID2Ref.end() ? -1 : ++( it->second ) );
	};

	// Referenzzähler eines Objektes dekrementieren (gibt Wert nach Dekrementierung zurück, Rückgabe -1 falls ID ungültig)
	inline int DecRefCount( int iID )
	{
		typename std::map<int, int>::iterator it = m_mID2Ref.find( iID );
		return ( it == m_mID2Ref.end() ? -1 : ( it->second > 0 ? --( it->second ) : 0 ) );
	};

	//! Wie GetObject(), erhöht aber direkt den Referenzzähler
	inline T* Request( const int iID )
	{
		T* pObject = GetObject( iID );

		// Gültige ID? Dann Referenzzähler erhöhen
		if( pObject )
			IncRefCount( iID );

		return pObject;
	};

	// Alias für DecRefCount() (Rückgabewert: Verbleibende Anzahl Referenzen, Rückgabe -1 falls ID ungültig)
	inline int Release( int iID )
	{
		return DecRefCount( iID );
	};

	// Der Operator [] ist Alias für GetObject()
	inline T* operator[]( int iID ) const
	{
		return GetObject( iID );
	};

	// --= Realisierung des STL-Iterator-Patterns =--

	/*
	 *  Hinweis: Der Iterator arbeitet wie bei std::map über Paare (std::pair) von <int, T*>
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
	std::map< int, int > m_mID2Ref;	// Referenzzähler
};

#endif // IW_VACORE_OBJECTCONTAINER
