using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace FEDAL
{
    public class FEContext : DbContext
    {
        public FEContext() : base("OracleDbContext")
        { }

        public static FEContext Create()
        {
            return new FEContext();
        }

        public DbSet<Buzon> Buzones { get; set; }
        public DbSet<DocumentoAutorizado> DocumentoAutorizado { get; set; }
        public DbSet<DocumentoRechazado> DocumentoRechazado { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // MUST go first.

            modelBuilder.HasDefaultSchema("FE"); // Use uppercase!


            //modelBuilder.Entity<Buzon>().ToTable("BUZON");
            modelBuilder.Entity<Buzon>().Property(p => p.FechaHora).HasPrecision(6);

            //modelBuilder.Entity<DocumentoAutorizado>().ToTable("DOCUMENTO_AUTORIZADO");
            modelBuilder.Entity<DocumentoAutorizado>().Property(p => p.FechaHoraAutorizacion).HasPrecision(6);

           // modelBuilder.Entity<DocumentoEventos>().ToTable("DOCUMENTOS_EVENTOS");
            modelBuilder.Entity<DocumentoEventos>().Property(p => p.FechaHoraEvento).HasPrecision(6);

           // modelBuilder.Entity<DocumentoRechazado>().ToTable("RECHAZOS");
            modelBuilder.Entity<DocumentoRechazado>().Property(p => p.FechaHoraRechazo).HasPrecision(6);

            //modelBuilder.Entity<RucAcreditado>().ToTable("RUCS_ACREDITADOS");
            modelBuilder.Entity<RucAcreditado>().Property(p => p.FechaHoraActualizacion).HasPrecision(6);

        }
    }
}

