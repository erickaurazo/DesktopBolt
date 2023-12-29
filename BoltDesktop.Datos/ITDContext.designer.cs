﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Asistencia.Datos
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="SATURNO")]
	public partial class ITDContextDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertSAS_DispositivoImagene(SAS_DispositivoImagene instance);
    partial void UpdateSAS_DispositivoImagene(SAS_DispositivoImagene instance);
    partial void DeleteSAS_DispositivoImagene(SAS_DispositivoImagene instance);
    #endregion
		
		public ITDContextDataContext() : 
				base(global::Asistencia.Datos.Properties.Settings.Default.SATURNOConnectionString8, mappingSource)
		{
			OnCreated();
		}
		
		public ITDContextDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ITDContextDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ITDContextDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ITDContextDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<SAS_DispositivoImagene> SAS_DispositivoImagenes
		{
			get
			{
				return this.GetTable<SAS_DispositivoImagene>();
			}
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.SAS_EsPropioCBO")]
		public ISingleResult<SAS_EsPropioCBOResult> SAS_EsPropioCBO()
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
			return ((ISingleResult<SAS_EsPropioCBOResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.SAS_EstadoDispositivoCBO")]
		public ISingleResult<SAS_EstadoDispositivoCBOResult> SAS_EstadoDispositivoCBO()
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
			return ((ISingleResult<SAS_EstadoDispositivoCBOResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.SAS_FuncionamientoDispositivoCBO")]
		public ISingleResult<SAS_FuncionamientoDispositivoCBOResult> SAS_FuncionamientoDispositivoCBO()
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
			return ((ISingleResult<SAS_FuncionamientoDispositivoCBOResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.SAS_MarcaDispositivoCBO")]
		public ISingleResult<SAS_MarcaDispositivoCBOResult> SAS_MarcaDispositivoCBO()
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
			return ((ISingleResult<SAS_MarcaDispositivoCBOResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.SAS_ModeloDispositivoCBO")]
		public ISingleResult<SAS_ModeloDispositivoCBOResult> SAS_ModeloDispositivoCBO()
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
			return ((ISingleResult<SAS_ModeloDispositivoCBOResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.SAS_ProveedorDispositivoCBO")]
		public ISingleResult<SAS_ProveedorDispositivoCBOResult> SAS_ProveedorDispositivoCBO()
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
			return ((ISingleResult<SAS_ProveedorDispositivoCBOResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.SAS_SedeDispositivoCBO")]
		public ISingleResult<SAS_SedeDispositivoCBOResult> SAS_SedeDispositivoCBO()
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
			return ((ISingleResult<SAS_SedeDispositivoCBOResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.SAS_TipoDispositivoCBO")]
		public ISingleResult<SAS_TipoDispositivoCBOResult> SAS_TipoDispositivoCBO()
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
			return ((ISingleResult<SAS_TipoDispositivoCBOResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.SAS_ListadoDeDispositivosAll")]
		public ISingleResult<SAS_ListadoDeDispositivosAllResult> SAS_ListadoDeDispositivosAll()
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
			return ((ISingleResult<SAS_ListadoDeDispositivosAllResult>)(result.ReturnValue));
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.SAS_DispositivoImagenes")]
	public partial class SAS_DispositivoImagene : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _DispositivoId;
		
		private int _Item;
		
		private byte _EsPrincipal;
		
		private string _Ruta;
		
		private System.DateTime _Fecha;
		
		private string _Latitud;
		
		private string _Longitud;
		
		private string _Nota;
		
		private byte _Estado;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnDispositivoIdChanging(int value);
    partial void OnDispositivoIdChanged();
    partial void OnItemChanging(int value);
    partial void OnItemChanged();
    partial void OnEsPrincipalChanging(byte value);
    partial void OnEsPrincipalChanged();
    partial void OnRutaChanging(string value);
    partial void OnRutaChanged();
    partial void OnFechaChanging(System.DateTime value);
    partial void OnFechaChanged();
    partial void OnLatitudChanging(string value);
    partial void OnLatitudChanged();
    partial void OnLongitudChanging(string value);
    partial void OnLongitudChanged();
    partial void OnNotaChanging(string value);
    partial void OnNotaChanged();
    partial void OnEstadoChanging(byte value);
    partial void OnEstadoChanged();
    #endregion
		
		public SAS_DispositivoImagene()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DispositivoId", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int DispositivoId
		{
			get
			{
				return this._DispositivoId;
			}
			set
			{
				if ((this._DispositivoId != value))
				{
					this.OnDispositivoIdChanging(value);
					this.SendPropertyChanging();
					this._DispositivoId = value;
					this.SendPropertyChanged("DispositivoId");
					this.OnDispositivoIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Item", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Item
		{
			get
			{
				return this._Item;
			}
			set
			{
				if ((this._Item != value))
				{
					this.OnItemChanging(value);
					this.SendPropertyChanging();
					this._Item = value;
					this.SendPropertyChanged("Item");
					this.OnItemChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EsPrincipal", DbType="TinyInt NOT NULL")]
		public byte EsPrincipal
		{
			get
			{
				return this._EsPrincipal;
			}
			set
			{
				if ((this._EsPrincipal != value))
				{
					this.OnEsPrincipalChanging(value);
					this.SendPropertyChanging();
					this._EsPrincipal = value;
					this.SendPropertyChanged("EsPrincipal");
					this.OnEsPrincipalChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Ruta", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
		public string Ruta
		{
			get
			{
				return this._Ruta;
			}
			set
			{
				if ((this._Ruta != value))
				{
					this.OnRutaChanging(value);
					this.SendPropertyChanging();
					this._Ruta = value;
					this.SendPropertyChanged("Ruta");
					this.OnRutaChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Fecha", DbType="DateTime NOT NULL")]
		public System.DateTime Fecha
		{
			get
			{
				return this._Fecha;
			}
			set
			{
				if ((this._Fecha != value))
				{
					this.OnFechaChanging(value);
					this.SendPropertyChanging();
					this._Fecha = value;
					this.SendPropertyChanged("Fecha");
					this.OnFechaChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Latitud", DbType="VarChar(50)")]
		public string Latitud
		{
			get
			{
				return this._Latitud;
			}
			set
			{
				if ((this._Latitud != value))
				{
					this.OnLatitudChanging(value);
					this.SendPropertyChanging();
					this._Latitud = value;
					this.SendPropertyChanged("Latitud");
					this.OnLatitudChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Longitud", DbType="VarChar(50)")]
		public string Longitud
		{
			get
			{
				return this._Longitud;
			}
			set
			{
				if ((this._Longitud != value))
				{
					this.OnLongitudChanging(value);
					this.SendPropertyChanging();
					this._Longitud = value;
					this.SendPropertyChanged("Longitud");
					this.OnLongitudChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Nota", DbType="VarChar(250)")]
		public string Nota
		{
			get
			{
				return this._Nota;
			}
			set
			{
				if ((this._Nota != value))
				{
					this.OnNotaChanging(value);
					this.SendPropertyChanging();
					this._Nota = value;
					this.SendPropertyChanged("Nota");
					this.OnNotaChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Estado", DbType="TinyInt NOT NULL")]
		public byte Estado
		{
			get
			{
				return this._Estado;
			}
			set
			{
				if ((this._Estado != value))
				{
					this.OnEstadoChanging(value);
					this.SendPropertyChanging();
					this._Estado = value;
					this.SendPropertyChanged("Estado");
					this.OnEstadoChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	public partial class SAS_EsPropioCBOResult
	{
		
		private byte _codigo;
		
		private string _descripcion;
		
		public SAS_EsPropioCBOResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_codigo", DbType="TinyInt NOT NULL")]
		public byte codigo
		{
			get
			{
				return this._codigo;
			}
			set
			{
				if ((this._codigo != value))
				{
					this._codigo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_descripcion", DbType="VarChar(7)")]
		public string descripcion
		{
			get
			{
				return this._descripcion;
			}
			set
			{
				if ((this._descripcion != value))
				{
					this._descripcion = value;
				}
			}
		}
	}
	
	public partial class SAS_EstadoDispositivoCBOResult
	{
		
		private System.Nullable<byte> _codigo;
		
		private string _descripcion;
		
		public SAS_EstadoDispositivoCBOResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_codigo", DbType="TinyInt")]
		public System.Nullable<byte> codigo
		{
			get
			{
				return this._codigo;
			}
			set
			{
				if ((this._codigo != value))
				{
					this._codigo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_descripcion", DbType="VarChar(80)")]
		public string descripcion
		{
			get
			{
				return this._descripcion;
			}
			set
			{
				if ((this._descripcion != value))
				{
					this._descripcion = value;
				}
			}
		}
	}
	
	public partial class SAS_FuncionamientoDispositivoCBOResult
	{
		
		private decimal _codigo;
		
		private string _descripcion;
		
		public SAS_FuncionamientoDispositivoCBOResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_codigo", DbType="Decimal(1,0) NOT NULL")]
		public decimal codigo
		{
			get
			{
				return this._codigo;
			}
			set
			{
				if ((this._codigo != value))
				{
					this._codigo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_descripcion", DbType="VarChar(12)")]
		public string descripcion
		{
			get
			{
				return this._descripcion;
			}
			set
			{
				if ((this._descripcion != value))
				{
					this._descripcion = value;
				}
			}
		}
	}
	
	public partial class SAS_MarcaDispositivoCBOResult
	{
		
		private string _codigo;
		
		private string _descripcion;
		
		public SAS_MarcaDispositivoCBOResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_codigo", DbType="Char(4) NOT NULL", CanBeNull=false)]
		public string codigo
		{
			get
			{
				return this._codigo;
			}
			set
			{
				if ((this._codigo != value))
				{
					this._codigo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_descripcion", DbType="VarChar(50)")]
		public string descripcion
		{
			get
			{
				return this._descripcion;
			}
			set
			{
				if ((this._descripcion != value))
				{
					this._descripcion = value;
				}
			}
		}
	}
	
	public partial class SAS_ModeloDispositivoCBOResult
	{
		
		private string _codigo;
		
		private string _descripcion;
		
		public SAS_ModeloDispositivoCBOResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_codigo", DbType="Char(4) NOT NULL", CanBeNull=false)]
		public string codigo
		{
			get
			{
				return this._codigo;
			}
			set
			{
				if ((this._codigo != value))
				{
					this._codigo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_descripcion", DbType="VarChar(80)")]
		public string descripcion
		{
			get
			{
				return this._descripcion;
			}
			set
			{
				if ((this._descripcion != value))
				{
					this._descripcion = value;
				}
			}
		}
	}
	
	public partial class SAS_ProveedorDispositivoCBOResult
	{
		
		private string _codigo;
		
		private string _descripcion;
		
		public SAS_ProveedorDispositivoCBOResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_codigo", DbType="VarChar(12) NOT NULL", CanBeNull=false)]
		public string codigo
		{
			get
			{
				return this._codigo;
			}
			set
			{
				if ((this._codigo != value))
				{
					this._codigo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_descripcion", DbType="VarChar(200)")]
		public string descripcion
		{
			get
			{
				return this._descripcion;
			}
			set
			{
				if ((this._descripcion != value))
				{
					this._descripcion = value;
				}
			}
		}
	}
	
	public partial class SAS_SedeDispositivoCBOResult
	{
		
		private string _codigo;
		
		private string _descripcion;
		
		public SAS_SedeDispositivoCBOResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_codigo", DbType="VarChar(3)")]
		public string codigo
		{
			get
			{
				return this._codigo;
			}
			set
			{
				if ((this._codigo != value))
				{
					this._codigo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_descripcion", DbType="VarChar(70)")]
		public string descripcion
		{
			get
			{
				return this._descripcion;
			}
			set
			{
				if ((this._descripcion != value))
				{
					this._descripcion = value;
				}
			}
		}
	}
	
	public partial class SAS_TipoDispositivoCBOResult
	{
		
		private string _codigo;
		
		private string _descripcion;
		
		public SAS_TipoDispositivoCBOResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_codigo", DbType="Char(3) NOT NULL", CanBeNull=false)]
		public string codigo
		{
			get
			{
				return this._codigo;
			}
			set
			{
				if ((this._codigo != value))
				{
					this._codigo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_descripcion", DbType="VarChar(255)")]
		public string descripcion
		{
			get
			{
				return this._descripcion;
			}
			set
			{
				if ((this._descripcion != value))
				{
					this._descripcion = value;
				}
			}
		}
	}
	
	public partial class SAS_ListadoDeDispositivosAllResult
	{
		
		private int _id;
		
		private string _latitud;
		
		private string _longitud;
		
		private string _nombres;
		
		private string _dispositivo;
		
		private string _sedeCodigo;
		
		private string _sedeDescripcion;
		
		private string _numeroSerie;
		
		private string _caracteristicas;
		
		private System.Nullable<byte> _idestado;
		
		private string _tipoDispositivoCodigo;
		
		private string _tipoDispositivo;
		
		private string _estado;
		
		private string _item;
		
		private string _codigoSegmentoIP;
		
		private string _numeroIP;
		
		private string _creadoPor;
		
		private System.Nullable<System.DateTime> _fechacreacion;
		
		private string _activoCodigoERP;
		
		private string _activo;
		
		private string _IdDispostivoColor;
		
		private string _color;
		
		private string _idModelo;
		
		private string _MODELO;
		
		private string _idMarca;
		
		private string _marca;
		
		private string _numeroParte;
		
		private char _IdEstadoProducto;
		
		private string _estadoProducto;
		
		private byte _EsPropio;
		
		private string _AlquiladoPropio;
		
		private string _idProducto;
		
		private string _producto;
		
		private string _rutaImagen;
		
		private decimal _funcionamientoCodigo;
		
		private string _funcionamiento;
		
		private string _idClieprov;
		
		private string _razonSocial;
		
		private string _coordenada;
		
		private System.Nullable<System.DateTime> _fechaActivacion;
		
		private string _idCobrarpagarDoc;
		
		private string _documentoCompra;
		
		private System.Nullable<System.DateTime> _fechaBaja;
		
		private System.Nullable<System.DateTime> _fechaProduccion;
		
		private decimal _esFinal;
		
		private int _registro;
		
		private string _RegistradoNoRegistrado;
		
		private string _idarea;
		
		private string _area;
		
		private System.Data.Linq.Binary _imagen;
		
		private string _lineaCelular;
		
		private string _planDeTelefonia;
		
		private decimal _costoUSD;
		
		private string _ubicacion;
		
		private decimal _AniosParaDepreciar;
		
		private int _idSistemaDeImpresion;
		
		private string _sistemaDeImpresion;
		
		private int _ContabilizacionDeRegistro;
		
		private decimal _costoMantenimientoAnualUSD;
		
		private decimal _costoSuministroAnualUSD;
		
		private decimal _kilovatioHora;
		
		private char _tipoDeFacturacionDeConsumoEnergetico;
		
		private string _idcodigoGeneral;
		
		private string _ColaboradorUnicoAsociado;
		
		public SAS_ListadoDeDispositivosAllResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", DbType="Int NOT NULL")]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_latitud", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string latitud
		{
			get
			{
				return this._latitud;
			}
			set
			{
				if ((this._latitud != value))
				{
					this._latitud = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_longitud", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string longitud
		{
			get
			{
				return this._longitud;
			}
			set
			{
				if ((this._longitud != value))
				{
					this._longitud = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_nombres", DbType="NVarChar(MAX)")]
		public string nombres
		{
			get
			{
				return this._nombres;
			}
			set
			{
				if ((this._nombres != value))
				{
					this._nombres = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_dispositivo", DbType="NVarChar(MAX)")]
		public string dispositivo
		{
			get
			{
				return this._dispositivo;
			}
			set
			{
				if ((this._dispositivo != value))
				{
					this._dispositivo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_sedeCodigo", DbType="VarChar(3)")]
		public string sedeCodigo
		{
			get
			{
				return this._sedeCodigo;
			}
			set
			{
				if ((this._sedeCodigo != value))
				{
					this._sedeCodigo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_sedeDescripcion", DbType="VarChar(70)")]
		public string sedeDescripcion
		{
			get
			{
				return this._sedeDescripcion;
			}
			set
			{
				if ((this._sedeDescripcion != value))
				{
					this._sedeDescripcion = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_numeroSerie", DbType="NVarChar(MAX)")]
		public string numeroSerie
		{
			get
			{
				return this._numeroSerie;
			}
			set
			{
				if ((this._numeroSerie != value))
				{
					this._numeroSerie = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_caracteristicas", DbType="VarChar(MAX)")]
		public string caracteristicas
		{
			get
			{
				return this._caracteristicas;
			}
			set
			{
				if ((this._caracteristicas != value))
				{
					this._caracteristicas = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_idestado", DbType="TinyInt")]
		public System.Nullable<byte> idestado
		{
			get
			{
				return this._idestado;
			}
			set
			{
				if ((this._idestado != value))
				{
					this._idestado = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_tipoDispositivoCodigo", DbType="Char(3) NOT NULL", CanBeNull=false)]
		public string tipoDispositivoCodigo
		{
			get
			{
				return this._tipoDispositivoCodigo;
			}
			set
			{
				if ((this._tipoDispositivoCodigo != value))
				{
					this._tipoDispositivoCodigo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_tipoDispositivo", DbType="VarChar(255)")]
		public string tipoDispositivo
		{
			get
			{
				return this._tipoDispositivo;
			}
			set
			{
				if ((this._tipoDispositivo != value))
				{
					this._tipoDispositivo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_estado", DbType="VarChar(80)")]
		public string estado
		{
			get
			{
				return this._estado;
			}
			set
			{
				if ((this._estado != value))
				{
					this._estado = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_item", DbType="VarChar(2512) NOT NULL", CanBeNull=false)]
		public string item
		{
			get
			{
				return this._item;
			}
			set
			{
				if ((this._item != value))
				{
					this._item = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_codigoSegmentoIP", DbType="VarChar(1) NOT NULL", CanBeNull=false)]
		public string codigoSegmentoIP
		{
			get
			{
				return this._codigoSegmentoIP;
			}
			set
			{
				if ((this._codigoSegmentoIP != value))
				{
					this._codigoSegmentoIP = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_numeroIP", DbType="VarChar(132) NOT NULL", CanBeNull=false)]
		public string numeroIP
		{
			get
			{
				return this._numeroIP;
			}
			set
			{
				if ((this._numeroIP != value))
				{
					this._numeroIP = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_creadoPor", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string creadoPor
		{
			get
			{
				return this._creadoPor;
			}
			set
			{
				if ((this._creadoPor != value))
				{
					this._creadoPor = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_fechacreacion", DbType="SmallDateTime")]
		public System.Nullable<System.DateTime> fechacreacion
		{
			get
			{
				return this._fechacreacion;
			}
			set
			{
				if ((this._fechacreacion != value))
				{
					this._fechacreacion = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_activoCodigoERP", DbType="VarChar(22)")]
		public string activoCodigoERP
		{
			get
			{
				return this._activoCodigoERP;
			}
			set
			{
				if ((this._activoCodigoERP != value))
				{
					this._activoCodigoERP = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_activo", DbType="VarChar(200) NOT NULL", CanBeNull=false)]
		public string activo
		{
			get
			{
				return this._activo;
			}
			set
			{
				if ((this._activo != value))
				{
					this._activo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdDispostivoColor", DbType="Char(3) NOT NULL", CanBeNull=false)]
		public string IdDispostivoColor
		{
			get
			{
				return this._IdDispostivoColor;
			}
			set
			{
				if ((this._IdDispostivoColor != value))
				{
					this._IdDispostivoColor = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_color", DbType="VarChar(100)")]
		public string color
		{
			get
			{
				return this._color;
			}
			set
			{
				if ((this._color != value))
				{
					this._color = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_idModelo", DbType="Char(4) NOT NULL", CanBeNull=false)]
		public string idModelo
		{
			get
			{
				return this._idModelo;
			}
			set
			{
				if ((this._idModelo != value))
				{
					this._idModelo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MODELO", DbType="VarChar(80)")]
		public string MODELO
		{
			get
			{
				return this._MODELO;
			}
			set
			{
				if ((this._MODELO != value))
				{
					this._MODELO = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_idMarca", DbType="Char(4) NOT NULL", CanBeNull=false)]
		public string idMarca
		{
			get
			{
				return this._idMarca;
			}
			set
			{
				if ((this._idMarca != value))
				{
					this._idMarca = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_marca", DbType="VarChar(50)")]
		public string marca
		{
			get
			{
				return this._marca;
			}
			set
			{
				if ((this._marca != value))
				{
					this._marca = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_numeroParte", DbType="VarChar(100) NOT NULL", CanBeNull=false)]
		public string numeroParte
		{
			get
			{
				return this._numeroParte;
			}
			set
			{
				if ((this._numeroParte != value))
				{
					this._numeroParte = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdEstadoProducto", DbType="Char(1) NOT NULL")]
		public char IdEstadoProducto
		{
			get
			{
				return this._IdEstadoProducto;
			}
			set
			{
				if ((this._IdEstadoProducto != value))
				{
					this._IdEstadoProducto = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_estadoProducto", DbType="VarChar(10)")]
		public string estadoProducto
		{
			get
			{
				return this._estadoProducto;
			}
			set
			{
				if ((this._estadoProducto != value))
				{
					this._estadoProducto = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EsPropio", DbType="TinyInt NOT NULL")]
		public byte EsPropio
		{
			get
			{
				return this._EsPropio;
			}
			set
			{
				if ((this._EsPropio != value))
				{
					this._EsPropio = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AlquiladoPropio", DbType="VarChar(9)")]
		public string AlquiladoPropio
		{
			get
			{
				return this._AlquiladoPropio;
			}
			set
			{
				if ((this._AlquiladoPropio != value))
				{
					this._AlquiladoPropio = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_idProducto", DbType="VarChar(22) NOT NULL", CanBeNull=false)]
		public string idProducto
		{
			get
			{
				return this._idProducto;
			}
			set
			{
				if ((this._idProducto != value))
				{
					this._idProducto = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_producto", DbType="VarChar(240) NOT NULL", CanBeNull=false)]
		public string producto
		{
			get
			{
				return this._producto;
			}
			set
			{
				if ((this._producto != value))
				{
					this._producto = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_rutaImagen", DbType="VarChar(255) NOT NULL", CanBeNull=false)]
		public string rutaImagen
		{
			get
			{
				return this._rutaImagen;
			}
			set
			{
				if ((this._rutaImagen != value))
				{
					this._rutaImagen = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_funcionamientoCodigo", DbType="Decimal(1,0) NOT NULL")]
		public decimal funcionamientoCodigo
		{
			get
			{
				return this._funcionamientoCodigo;
			}
			set
			{
				if ((this._funcionamientoCodigo != value))
				{
					this._funcionamientoCodigo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_funcionamiento", DbType="VarChar(12)")]
		public string funcionamiento
		{
			get
			{
				return this._funcionamiento;
			}
			set
			{
				if ((this._funcionamiento != value))
				{
					this._funcionamiento = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_idClieprov", DbType="VarChar(12) NOT NULL", CanBeNull=false)]
		public string idClieprov
		{
			get
			{
				return this._idClieprov;
			}
			set
			{
				if ((this._idClieprov != value))
				{
					this._idClieprov = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_razonSocial", DbType="VarChar(200) NOT NULL", CanBeNull=false)]
		public string razonSocial
		{
			get
			{
				return this._razonSocial;
			}
			set
			{
				if ((this._razonSocial != value))
				{
					this._razonSocial = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_coordenada", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string coordenada
		{
			get
			{
				return this._coordenada;
			}
			set
			{
				if ((this._coordenada != value))
				{
					this._coordenada = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_fechaActivacion", DbType="SmallDateTime")]
		public System.Nullable<System.DateTime> fechaActivacion
		{
			get
			{
				return this._fechaActivacion;
			}
			set
			{
				if ((this._fechaActivacion != value))
				{
					this._fechaActivacion = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_idCobrarpagarDoc", DbType="VarChar(17) NOT NULL", CanBeNull=false)]
		public string idCobrarpagarDoc
		{
			get
			{
				return this._idCobrarpagarDoc;
			}
			set
			{
				if ((this._idCobrarpagarDoc != value))
				{
					this._idCobrarpagarDoc = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_documentoCompra", DbType="VarChar(54) NOT NULL", CanBeNull=false)]
		public string documentoCompra
		{
			get
			{
				return this._documentoCompra;
			}
			set
			{
				if ((this._documentoCompra != value))
				{
					this._documentoCompra = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_fechaBaja", DbType="SmallDateTime")]
		public System.Nullable<System.DateTime> fechaBaja
		{
			get
			{
				return this._fechaBaja;
			}
			set
			{
				if ((this._fechaBaja != value))
				{
					this._fechaBaja = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_fechaProduccion", DbType="SmallDateTime")]
		public System.Nullable<System.DateTime> fechaProduccion
		{
			get
			{
				return this._fechaProduccion;
			}
			set
			{
				if ((this._fechaProduccion != value))
				{
					this._fechaProduccion = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_esFinal", DbType="Decimal(1,0) NOT NULL")]
		public decimal esFinal
		{
			get
			{
				return this._esFinal;
			}
			set
			{
				if ((this._esFinal != value))
				{
					this._esFinal = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_registro", DbType="Int NOT NULL")]
		public int registro
		{
			get
			{
				return this._registro;
			}
			set
			{
				if ((this._registro != value))
				{
					this._registro = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RegistradoNoRegistrado", DbType="VarChar(13) NOT NULL", CanBeNull=false)]
		public string RegistradoNoRegistrado
		{
			get
			{
				return this._RegistradoNoRegistrado;
			}
			set
			{
				if ((this._RegistradoNoRegistrado != value))
				{
					this._RegistradoNoRegistrado = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_idarea", DbType="Char(3) NOT NULL", CanBeNull=false)]
		public string idarea
		{
			get
			{
				return this._idarea;
			}
			set
			{
				if ((this._idarea != value))
				{
					this._idarea = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_area", DbType="VarChar(100)")]
		public string area
		{
			get
			{
				return this._area;
			}
			set
			{
				if ((this._area != value))
				{
					this._area = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_imagen", DbType="Image")]
		public System.Data.Linq.Binary imagen
		{
			get
			{
				return this._imagen;
			}
			set
			{
				if ((this._imagen != value))
				{
					this._imagen = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_lineaCelular", DbType="VarChar(10) NOT NULL", CanBeNull=false)]
		public string lineaCelular
		{
			get
			{
				return this._lineaCelular;
			}
			set
			{
				if ((this._lineaCelular != value))
				{
					this._lineaCelular = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_planDeTelefonia", DbType="VarChar(150) NOT NULL", CanBeNull=false)]
		public string planDeTelefonia
		{
			get
			{
				return this._planDeTelefonia;
			}
			set
			{
				if ((this._planDeTelefonia != value))
				{
					this._planDeTelefonia = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_costoUSD", DbType="Decimal(12,3) NOT NULL")]
		public decimal costoUSD
		{
			get
			{
				return this._costoUSD;
			}
			set
			{
				if ((this._costoUSD != value))
				{
					this._costoUSD = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ubicacion", DbType="VarChar(200) NOT NULL", CanBeNull=false)]
		public string ubicacion
		{
			get
			{
				return this._ubicacion;
			}
			set
			{
				if ((this._ubicacion != value))
				{
					this._ubicacion = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AniosParaDepreciar", DbType="Decimal(2,0) NOT NULL")]
		public decimal AniosParaDepreciar
		{
			get
			{
				return this._AniosParaDepreciar;
			}
			set
			{
				if ((this._AniosParaDepreciar != value))
				{
					this._AniosParaDepreciar = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_idSistemaDeImpresion", DbType="Int NOT NULL")]
		public int idSistemaDeImpresion
		{
			get
			{
				return this._idSistemaDeImpresion;
			}
			set
			{
				if ((this._idSistemaDeImpresion != value))
				{
					this._idSistemaDeImpresion = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_sistemaDeImpresion", DbType="VarChar(150)")]
		public string sistemaDeImpresion
		{
			get
			{
				return this._sistemaDeImpresion;
			}
			set
			{
				if ((this._sistemaDeImpresion != value))
				{
					this._sistemaDeImpresion = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ContabilizacionDeRegistro", DbType="Int NOT NULL")]
		public int ContabilizacionDeRegistro
		{
			get
			{
				return this._ContabilizacionDeRegistro;
			}
			set
			{
				if ((this._ContabilizacionDeRegistro != value))
				{
					this._ContabilizacionDeRegistro = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_costoMantenimientoAnualUSD", DbType="Decimal(12,3) NOT NULL")]
		public decimal costoMantenimientoAnualUSD
		{
			get
			{
				return this._costoMantenimientoAnualUSD;
			}
			set
			{
				if ((this._costoMantenimientoAnualUSD != value))
				{
					this._costoMantenimientoAnualUSD = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_costoSuministroAnualUSD", DbType="Decimal(12,3) NOT NULL")]
		public decimal costoSuministroAnualUSD
		{
			get
			{
				return this._costoSuministroAnualUSD;
			}
			set
			{
				if ((this._costoSuministroAnualUSD != value))
				{
					this._costoSuministroAnualUSD = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_kilovatioHora", DbType="Decimal(12,3) NOT NULL")]
		public decimal kilovatioHora
		{
			get
			{
				return this._kilovatioHora;
			}
			set
			{
				if ((this._kilovatioHora != value))
				{
					this._kilovatioHora = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_tipoDeFacturacionDeConsumoEnergetico", DbType="Char(1) NOT NULL")]
		public char tipoDeFacturacionDeConsumoEnergetico
		{
			get
			{
				return this._tipoDeFacturacionDeConsumoEnergetico;
			}
			set
			{
				if ((this._tipoDeFacturacionDeConsumoEnergetico != value))
				{
					this._tipoDeFacturacionDeConsumoEnergetico = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_idcodigoGeneral", DbType="VarChar(12) NOT NULL", CanBeNull=false)]
		public string idcodigoGeneral
		{
			get
			{
				return this._idcodigoGeneral;
			}
			set
			{
				if ((this._idcodigoGeneral != value))
				{
					this._idcodigoGeneral = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ColaboradorUnicoAsociado", DbType="VarChar(250) NOT NULL", CanBeNull=false)]
		public string ColaboradorUnicoAsociado
		{
			get
			{
				return this._ColaboradorUnicoAsociado;
			}
			set
			{
				if ((this._ColaboradorUnicoAsociado != value))
				{
					this._ColaboradorUnicoAsociado = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
