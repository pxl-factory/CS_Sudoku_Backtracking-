﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Sudoku.Model
{

    public class Groupe : HashSet<Cellule>
    {

        #region Propriétés
        /// <summary>
        /// Numéro du groupe (entre 0 et 8)
        /// </summary>
        public virtual int Numéro { get; }
        /// <summary>
        ///  Nom du groupe
        /// </summary>
        public virtual string Nom { get; private set; }
        /// <summary>
        /// Nom court du groupe
        /// </summary>
        public virtual string NomCourt { get; private set; }
        /// <summary>
        /// Ensemble des valeurs du groupe qui sont trouvés (une cellule du groupe dans l'état trouvé 
        /// contient cette valeur) ou fixées (une cellule du groupe dans l'état Fixé contient cette valeur).
        /// </summary>
        public SortedSet<int> ValeursFixéesOuTrouvées { get; } = new SortedSet<int>();
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public Groupe() : base() { }
        /// <summary>
        /// Constructeur à partir d'une liste de cellules qui représentent les cellules de ce 
        /// groupe
        /// </summary>
        /// <param name="liste">Liste des cellules qui font parti de ce groupe</param>
        /// <remarks>Les cellules ne doivent pas être dupliquées ! Le groupe doit référencer
        /// directement ces cellules. </remarks>
        public Groupe(IEnumerable<Cellule> liste) : base(liste)
        {
            //foreach (Cellule copy in liste)
            //{
            //    Numéro = copy.;
            //    Nom = copy.Nom;
            //    NomCourt = copy.NomCourt;
            //    ValeursFixéesOuTrouvées = new SortedSet<int>(copy.Possibilités);



            //}
        }
        
        /// <summary>
        /// Constructeur par copie
        /// </summary>
        public Groupe(Groupe grp) : base(grp)
        {
            Numéro = grp.Numéro;
            ValeursFixéesOuTrouvées = new SortedSet<int>(grp.ValeursFixéesOuTrouvées);
            
        }
        
        /// <summary>
        /// Constructeur avec le numéro du groupe.
        /// 
        /// Le numéro du groupe doit être compris entre 0 et 8
        /// </summary>
        /// <param name="numéro">Numéro du groupe (entre 0 et 8)</param>
        /// <exception cref="ArgumentOutOfRangeException">si le numéro n'est pas compris entre 
        /// 0 (inclu) et 8 (inclu).</exception>
        public Groupe(int numéro) : base()
        {
            if(numéro >= 0 && numéro <= 8)
            {
            Numéro = numéro;
            }
            else {
            throw new ArgumentOutOfRangeException();
            }
        }
        #endregion

        #region Méthodes

        /// <summary>
        /// Permet d'ajouter une valeur aux valeurs fixées ou trouvées du groupe.
        /// </summary>
        /// <param name="v">Valeur à ajouter</param>
        /// <exception cref="ArgumentOutOfRangeException">Si la valeur n'est pas comprise entre 1 et 9</exception>
        public void AjouterValeurFixéeOuTrouvée(int v)
        {
            if(v >= 1 && v <= 9)
            {
                ValeursFixéesOuTrouvées.Add(v);
            }
            else
            {

            throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Permet de supprimer une valeur des valeurs fixées ou trouvées du groupe.
        /// </summary>
        /// <param name="v">Valeur à supprimer</param>
        /// <exception cref="ArgumentOutOfRangeException">Si la valeur n'est pas comprise entre 1 et 9</exception>
        public void SupprimerValeurFixéeOuTrouvée(int v)
        {
            if (v >= 1 && v <= 9)
            {
                ValeursFixéesOuTrouvées.Remove(v);
            }
            else
            {

                throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Détermine si la valeur donnée en paramètre fait partie des valeurs trouvées ou fixées
        /// de ce groupe.
        /// </summary>
        /// <param name="v">Valeur à rechercher</param>
        /// <returns><code>true</code> si la valeur est contenue dans les valeurs fixées ou trouvées
        /// du groupe, <code>false</code> sinon.</returns>
        public bool ContientValeurFixéeOuTrouvée(int v)
        {
            
            return ValeursFixéesOuTrouvées.Contains(v);
        }

        /// <summary>
        /// Détermine si au moins une des valeurs de la liste est une valeur fixée ou trouvée du groupe.
        /// </summary>
        /// <param name="ens">Liste des valeurs à rechercher</param>
        /// <returns><code>true</code> si au moins une des valeurs est une valeur fixée ou trouvée
        /// du groupe, <code>false</code> sinon.</returns>
        public bool ContientAuMoinsUneValeurFixéeOuTrouvée(IEnumerable<int> ens)
        {
            bool resp = false;
            foreach (var item in ens)
            {
                if (ValeursFixéesOuTrouvées.Contains(item))
                {
                    resp = true;
                }
            }
            return resp;    
         }

        /// <summary>
        /// Méthode permettant d'effacer toutes les cellules du groupe.
        /// <see cref="Cellule.Effacer"/>
        /// </summary>
        public void Effacer()
        {
            foreach (var c in this)
            {
                c.Effacer();
            }

        }

        /// <summary>
        /// Retourne la première cellule du groupe
        /// </summary>
        /// <returns>Première cellule du groupe</returns>
        public Cellule GetFirst()
        {
            return this.First();
        }

        /// <summary>
        /// Permet de remplir les possibilités de toutes les cellules contenues
        /// dans le groupe.
        /// </summary>
        /// <see cref="Cellule.RemplirPossibilités"/>
        public void RemplirPossibilités()
        {
            foreach (var c in this)
            {
                c.RemplirPossibilités();
            }
        }

        /// <summary>
        /// Permet de supprimer une valeur possible des possibilités
        /// de toutes les cellules sauf celle passée en paramètre.
        /// </summary>
        /// <param name="v">Valeur à supprimer des possibilités des cellules</param>
        /// <param name="origine">Cellule à ne pas modifier</param>
        /// <returns>Retourne <code>true</code> si au moins une cellule
        /// a été modifiée, <code>false</code> sinon.</returns>
        public bool SupprimerPossibilité(int v, Cellule origine = null)
        {
            bool resp = false;

            foreach (var c in this)
            {
                if (c.Contient(v) && c != origine)
                {
                    c.SupprimerPossibilité(v);
                    resp = true;
                }
            }
            return resp;
        }

        /// <summary>
        /// Permet de supprimer différentes valeurs des possibilités des cellules du groupe.
        /// </summary>
        /// <param name="vals">Valeurs à supprimer</param>
        /// <param name="origine">Cellule à ne pas modifier</param>
        /// <returns><code>true</code> si une des cellules au moins a été modifiée,
        /// <code>false</code> sinon.</returns>
        public bool SupprimerPossibilité(IEnumerable<int> vals, Cellule origine)
        {
            bool resp = false;
            foreach (var c in this)
            {
                foreach (var val in vals)
                {
                    if(c.Contient(val) && c != origine)
                    {
                        c.SupprimerPossibilité(val);
                        resp = true;
                    }
                }
            }
            return resp;
        }

        /// <summary>
        /// Permet de supprimer des valeurs des possibilités des cellules du groupe sauf celles
        /// contenues dans le groupe passé en paramètre.
        /// </summary>
        /// <param name="vals">Valeurs à supprimer</param>
        /// <param name="origines">Groupe de cellules à ne pas modifier</param>
        /// <returns><code>true</code> si au moins une cellule a été modifiée,
        /// <code>false</code> sinon.</returns>
        public bool SupprimerPossibilité(IEnumerable<int> vals, Groupe origines)
        {
            bool resp = false;
            foreach (var c in this)
            {
                foreach (var val in vals)
                {
                    foreach (var grp in origines)
                    {
                        if (!(grp.Contient(val)))
                        {
                            c.SupprimerPossibilité(val);
                            resp = true;
                        }
                    }
                }
            }
            return resp;
        }

        /// <summary>
        /// Permet de savoir si au moins une cellule du groupe contient la valeur 
        /// donnée en argument dans ses possibilités.
        /// </summary>
        /// <param name="v">Valeur à rechercher</param>
        /// <returns><code>true</code> si le groupe possède au moins une cellule contenant
        /// la valeur dans ses possibilités, <code>false</code> sinon.</returns>
        public bool Contient(int v)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retourne l'ensemble des cellules contenant au plus les valeurs données
        /// dans leur possibilités.
        /// </summary>
        /// <param name="vals">Valeurs à chercher dans les cellules.</param>
        /// <returns>Le groupe contenant les cellules possédant au plus les valeurs
        /// données en paramètre.</returns>
        public Groupe GetCellulesContenantAuPlus(IEnumerable<int> vals)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Compte le nombre de cellules du groupe qui sont ni "Trouvées", ni "Fixées"
        /// </summary>
        /// <returns>Nombre de cellules non "Trouvées" et non "Fixées"</returns>
        public int GetNbCellulesNonTrouvées()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Permet de récupérer les cellules du groupe qui contiennent les valeurs données en paramètre
        /// en tant que possibilités.
        /// </summary>
        /// <param name="vals">Valeurs à rechercher</param>
        /// <returns>Le groupe avec les cellules contenant les valeurs à rechercher.</returns>
        public Groupe GetCellulesContenantAuMoins(IEnumerable<int> vals)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region Opérateurs
        /// <summary>
        /// Crée un groupe contenant les cellules des deux groupes donnés en paramètre.
        /// Le nom du groupe sera : 
        ///     Union(nom_groupe_<code>g1</code>, nom_groupe_<code>g2</code>
        /// Le nom court sera : 
        ///     (nomCourt_groupe_<code>g1</code>, nomCourt_groupe_<code>g2</code>)
        /// </summary>
        /// <param name="g1">Premier groupe</param>
        /// <param name="g2">Secod groupe</param>
        /// <returns>Groupe contenant les cellules des groupes <code>g1</code>
        /// et <code>g2</code></returns>
        public static Groupe operator +(Groupe g1, Groupe g2)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retourne un groupe contenant les cellules du groupe <code>g1</code>
        /// dont on a retiré les cellules du groupe <code>g2</code>
        /// 
        /// Le nom du groupe sera : 
        ///     (nom_groupe_<code>g1</code> privé de nom_groupe_<code>g2</code>)
        ///     
        /// Le nom court sera : 
        ///     (nomCourt_groupe_<code>g1</code> \ nomCourt_groupe_<code>g2</code>)
        /// </summary>
        /// <param name="g1">Premier groupe</param>
        /// <param name="g2">Second groupe</param>
        /// <returns>Groupe contenant les cellules du premier groupe auquelles on
        /// a retiré les cellules du second groupe.</returns>
        public static Groupe operator -(Groupe g1, Groupe g2)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retourne un groupe contenant les cellules communes au groupe <code>g1</code>
        /// et au groupe <code>g2</code> (intersection).
        /// 
        /// Le nom sera : 
        ///     Intersect(nom_groupe_<code>g1</code>, nom_groupe_<code>g2</code>)
        ///     
        /// Le nom court sera : 
        /// </summary>
        ///     (nomCourt_groupe_<cpde>g1</cpde> n nomCourt_groupe_<code>g2</code>
        /// <param name="g1">Premier groupe</param>
        /// <param name="g2">Second groupe</param>
        /// <returns>Groupe constitué des cellules appartenant à la fois au premier
        /// et au second groupe (intersection)</returns>
        public static Groupe operator *(Groupe g1, Groupe g2)
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}
