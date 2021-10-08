using System;
using System.Collections.Generic;

namespace Faktur_gaji_pegawai_interface
{
    public interface HarusDibayar
    {
        decimal JumlahPembayaran();
    }
    public class Faktur : HarusDibayar
    {
        public string NomerBagian { get; }
        public string DeskripsiBagian { get; }
        private int kuantitas;
        private decimal hargaPerBarang;

        public Faktur(string nomerBagian, string deskripsiBagian, int kuantitas, decimal hargaPerBarang)
        {
            NomerBagian = nomerBagian;
            DeskripsiBagian = deskripsiBagian;
            Kuantitas = kuantitas;
            HargaPerBarang = hargaPerBarang;
        }
        public int Kuantitas
        {
            get
            {
                return kuantitas;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(Kuantitas)} must be >=0");
                }
                kuantitas = value;
            }
        }
        public decimal HargaPerBarang
        {
            get
            {
                return hargaPerBarang;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(HargaPerBarang)} must be >=0");
                }
                hargaPerBarang = value;
            }
        }
        public override string ToString() =>
            $"Faktur = \nNomer Bagian = {NomerBagian} ({DeskripsiBagian})\n" +
            $"Kuantitas = {Kuantitas}\nHarga Per Barang {HargaPerBarang:C}";

        public decimal JumlahPembayaran() => Kuantitas * HargaPerBarang;
    }
    public abstract class Pegawai : HarusDibayar
    {
        public string NamaDepan { get; }
        public string NamaBelakang { get; }
        public string NomerKTP { get; }

        public Pegawai(string namaDepan, string namaBelakang, string nomerKTP)
        {
            NamaDepan = namaDepan;
            NamaBelakang = namaBelakang;
            NomerKTP = nomerKTP;
        }
        public override string ToString() => $"{NamaDepan} {NamaBelakang}\n" + $"Nomer KTP = {NomerKTP}";
        public abstract decimal Pendapatan();
        public decimal JumlahPembayaran() => Pendapatan();
    }
    public class GajiPegawai : HarusDibayar
    {
        private decimal gajiMingguan;
        public GajiPegawai(string namaDepan, string namaBelakang, string nomerKTP, decimal gajiMingguan)
        {
            GajiMingguan = gajiMingguan;
        }
        public decimal GajiMingguan
        {
            get
            {
                return gajiMingguan;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                    value, $"{nameof(GajiMingguan)}must be>=0");
                }
                gajiMingguan = value;
            }
        }
        public override string ToString() => $"Gaji Mingguan = {GajiMingguan:C}\n";
        public decimal JumlahPembayaran() => GajiMingguan;
    }

        class JumlahPembayaranInterfaceTest
    {
        static void Main()
        {
            var YangDibayar = new List<HarusDibayar>()
            {
                new Faktur("01234","kursi",2,375.00M),
                new Faktur("56789","ban",4,79.95M),
                new GajiPegawai("John","Smith","111-11-1111",800.00M),
                new GajiPegawai("Lisa","Barnes","888-88-8888",1200.00M) };

            Console.WriteLine("Faktur dan Karyawan diproses secara polymophically\n");

            foreach(var Dibayar in YangDibayar)
            {
                Console.WriteLine($"{Dibayar}");
                Console.WriteLine($"Pembayaran Jatuh Tempo = {Dibayar.JumlahPembayaran():C}\n");
            }
            Console.ReadLine();
        }
    }
}
