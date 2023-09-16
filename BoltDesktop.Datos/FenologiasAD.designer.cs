﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="FENOLOGIAS")]
	public partial class FenologiasADDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Definiciones de métodos de extensibilidad
    partial void OnCreated();
    partial void InsertConsumidor(Consumidor instance);
    partial void UpdateConsumidor(Consumidor instance);
    partial void DeleteConsumidor(Consumidor instance);
    partial void InsertParametroConfiguracion(ParametroConfiguracion instance);
    partial void UpdateParametroConfiguracion(ParametroConfiguracion instance);
    partial void DeleteParametroConfiguracion(ParametroConfiguracion instance);
    #endregion
		
		public FenologiasADDataContext() : 
				base(global::Asistencia.Datos.Properties.Settings.Default.FENOLOGIASConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public FenologiasADDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public FenologiasADDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public FenologiasADDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public FenologiasADDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Consumidor> Consumidor
		{
			get
			{
				return this.GetTable<Consumidor>();
			}
		}
		
		public System.Data.Linq.Table<ParametroConfiguracion> ParametroConfiguracion
		{
			get
			{
				return this.GetTable<ParametroConfiguracion>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Consumidor")]
	public partial class Consumidor : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _EmpresaId;
		
		private string _LoteId;
		
		private System.Nullable<int> _Nivel;
		
		private string _Jerarquia;
		
		private string _Descripcion;
		
		private string _Abreviatura;
		
		private string _Erp01;
		
		private string _Erp02;
		
		private string _Erp03;
		
		private System.Nullable<byte> _EsFinal;
		
		private System.Nullable<byte> _Estado;
		
    #region Definiciones de métodos de extensibilidad
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnEmpresaIdChanging(string value);
    partial void OnEmpresaIdChanged();
    partial void OnLoteIdChanging(string value);
    partial void OnLoteIdChanged();
    partial void OnNivelChanging(System.Nullable<int> value);
    partial void OnNivelChanged();
    partial void OnJerarquiaChanging(string value);
    partial void OnJerarquiaChanged();
    partial void OnDescripcionChanging(string value);
    partial void OnDescripcionChanged();
    partial void OnAbreviaturaChanging(string value);
    partial void OnAbreviaturaChanged();
    partial void OnErp01Changing(string value);
    partial void OnErp01Changed();
    partial void OnErp02Changing(string value);
    partial void OnErp02Changed();
    partial void OnErp03Changing(string value);
    partial void OnErp03Changed();
    partial void OnEsFinalChanging(System.Nullable<byte> value);
    partial void OnEsFinalChanged();
    partial void OnEstadoChanging(System.Nullable<byte> value);
    partial void OnEstadoChanged();
    #endregion
		
		public Consumidor()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EmpresaId", DbType="Char(3) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string EmpresaId
		{
			get
			{
				return this._EmpresaId;
			}
			set
			{
				if ((this._EmpresaId != value))
				{
					this.OnEmpresaIdChanging(value);
					this.SendPropertyChanging();
					this._EmpresaId = value;
					this.SendPropertyChanged("EmpresaId");
					this.OnEmpresaIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LoteId", DbType="VarChar(12) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string LoteId
		{
			get
			{
				return this._LoteId;
			}
			set
			{
				if ((this._LoteId != value))
				{
					this.OnLoteIdChanging(value);
					this.SendPropertyChanging();
					this._LoteId = value;
					this.SendPropertyChanged("LoteId");
					this.OnLoteIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Nivel", DbType="Int")]
		public System.Nullable<int> Nivel
		{
			get
			{
				return this._Nivel;
			}
			set
			{
				if ((this._Nivel != value))
				{
					this.OnNivelChanging(value);
					this.SendPropertyChanging();
					this._Nivel = value;
					this.SendPropertyChanged("Nivel");
					this.OnNivelChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Jerarquia", DbType="VarChar(150)")]
		public string Jerarquia
		{
			get
			{
				return this._Jerarquia;
			}
			set
			{
				if ((this._Jerarquia != value))
				{
					this.OnJerarquiaChanging(value);
					this.SendPropertyChanging();
					this._Jerarquia = value;
					this.SendPropertyChanged("Jerarquia");
					this.OnJerarquiaChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Descripcion", DbType="VarChar(200)")]
		public string Descripcion
		{
			get
			{
				return this._Descripcion;
			}
			set
			{
				if ((this._Descripcion != value))
				{
					this.OnDescripcionChanging(value);
					this.SendPropertyChanging();
					this._Descripcion = value;
					this.SendPropertyChanged("Descripcion");
					this.OnDescripcionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Abreviatura", DbType="VarChar(20)")]
		public string Abreviatura
		{
			get
			{
				return this._Abreviatura;
			}
			set
			{
				if ((this._Abreviatura != value))
				{
					this.OnAbreviaturaChanging(value);
					this.SendPropertyChanging();
					this._Abreviatura = value;
					this.SendPropertyChanged("Abreviatura");
					this.OnAbreviaturaChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Erp01", DbType="VarChar(12)")]
		public string Erp01
		{
			get
			{
				return this._Erp01;
			}
			set
			{
				if ((this._Erp01 != value))
				{
					this.OnErp01Changing(value);
					this.SendPropertyChanging();
					this._Erp01 = value;
					this.SendPropertyChanged("Erp01");
					this.OnErp01Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Erp02", DbType="VarChar(12)")]
		public string Erp02
		{
			get
			{
				return this._Erp02;
			}
			set
			{
				if ((this._Erp02 != value))
				{
					this.OnErp02Changing(value);
					this.SendPropertyChanging();
					this._Erp02 = value;
					this.SendPropertyChanged("Erp02");
					this.OnErp02Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Erp03", DbType="VarChar(12)")]
		public string Erp03
		{
			get
			{
				return this._Erp03;
			}
			set
			{
				if ((this._Erp03 != value))
				{
					this.OnErp03Changing(value);
					this.SendPropertyChanging();
					this._Erp03 = value;
					this.SendPropertyChanged("Erp03");
					this.OnErp03Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EsFinal", DbType="TinyInt")]
		public System.Nullable<byte> EsFinal
		{
			get
			{
				return this._EsFinal;
			}
			set
			{
				if ((this._EsFinal != value))
				{
					this.OnEsFinalChanging(value);
					this.SendPropertyChanging();
					this._EsFinal = value;
					this.SendPropertyChanged("EsFinal");
					this.OnEsFinalChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Estado", DbType="TinyInt")]
		public System.Nullable<byte> Estado
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.ParametroConfiguracion")]
	public partial class ParametroConfiguracion : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _EmpresaId;
		
		private int _Id;
		
		private string _Descripcion;
		
		private string _Valor;
		
		private System.Nullable<decimal> _Cantidad;
		
    #region Definiciones de métodos de extensibilidad
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnEmpresaIdChanging(string value);
    partial void OnEmpresaIdChanged();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnDescripcionChanging(string value);
    partial void OnDescripcionChanged();
    partial void OnValorChanging(string value);
    partial void OnValorChanged();
    partial void OnCantidadChanging(System.Nullable<decimal> value);
    partial void OnCantidadChanged();
    #endregion
		
		public ParametroConfiguracion()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EmpresaId", DbType="Char(3) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string EmpresaId
		{
			get
			{
				return this._EmpresaId;
			}
			set
			{
				if ((this._EmpresaId != value))
				{
					this.OnEmpresaIdChanging(value);
					this.SendPropertyChanging();
					this._EmpresaId = value;
					this.SendPropertyChanged("EmpresaId");
					this.OnEmpresaIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Descripcion", DbType="VarChar(200)")]
		public string Descripcion
		{
			get
			{
				return this._Descripcion;
			}
			set
			{
				if ((this._Descripcion != value))
				{
					this.OnDescripcionChanging(value);
					this.SendPropertyChanging();
					this._Descripcion = value;
					this.SendPropertyChanged("Descripcion");
					this.OnDescripcionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Valor", DbType="VarChar(50)")]
		public string Valor
		{
			get
			{
				return this._Valor;
			}
			set
			{
				if ((this._Valor != value))
				{
					this.OnValorChanging(value);
					this.SendPropertyChanging();
					this._Valor = value;
					this.SendPropertyChanged("Valor");
					this.OnValorChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Cantidad", DbType="Decimal(12,3)")]
		public System.Nullable<decimal> Cantidad
		{
			get
			{
				return this._Cantidad;
			}
			set
			{
				if ((this._Cantidad != value))
				{
					this.OnCantidadChanging(value);
					this.SendPropertyChanging();
					this._Cantidad = value;
					this.SendPropertyChanged("Cantidad");
					this.OnCantidadChanged();
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
}
#pragma warning restore 1591
