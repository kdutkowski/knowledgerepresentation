\NeedsTeXFormat{LaTeX2e}[1995/12/01]
\ProvidesClass{mini}[2011/10/15 v0.3 Praca dyplomowa (MiNI PW)]
%------------------------------------------------------------------------------%
\RequirePackage{ifthen}
\ProcessOptions\relax
\LoadClass[a4paper,11pt,oneside,onecolumn]{report}
%------------------------------------------------------------------------------%
\RequirePackage{graphicx}
\graphicspath{{Pictures/}}
%------------------------------------------------------------------------------%
\RequirePackage{geometry}
\geometry{
  inner=25mm,
  outer=25mm,
  bindingoffset=5mm,
  top=20mm,
  bottom=15mm,  % 20mm
  includeheadfoot
}
\renewcommand{\chaptermark}[1]{\markboth{\thechapter.\enspace#1}{}}
\renewcommand{\sectionmark}[1]{\markright{\thesection.\enspace#1}}
%------------------------------------------------------------------------------%

%------------------------------------------------------------------------------%
\renewcommand{\@seccntformat}[1]{\csname the#1\endcsname.\enspace}
\renewcommand{\numberline}[1]{\hb@xt@\@tempdima{#1.\hfil}}
%------------------------------------------------------------------------------%
\RequirePackage[parfill]{parskip}[2001/04/09]
\RequirePackage{setspace}
\frenchspacing
%------------------------------------------------------------------------------%
\clubpenalty=10000
\widowpenalty=10000
\brokenpenalty=10000
%------------------------------------------------------------------------------%
\RequirePackage{amsmath}
\RequirePackage{amsthm}
\RequirePackage{amsfonts}
\RequirePackage{amssymb}
%------------------------------------------------------------------------------%
\RequirePackage[OT4]{polski}
\bibliographystyle{plplain}
%------------------------------------------------------------------------------%
\RequirePackage[unicode]{hyperref}
\hypersetup{
  bookmarksopen,
  bookmarksopenlevel=1,
  pdfborder={0 0 0},
  pdfdisplaydoctitle,
  pdfpagelayout={SinglePage},
  pdfstartview={FitH}
}
%------------------------------------------------------------------------------%
\def\@schapter#1{
\phantomsection\addcontentsline{toc}{chapter}{#1}
\markboth{#1}{#1}\@makeschapterhead{#1}\@afterheading}
%------------------------------------------------------------------------------%
\renewcommand{\tableofcontents}{
\if@openright\cleardoublepage\else\clearpage\fi\thispagestyle{plain}
\pdfbookmark[0]{\contentsname}{tableofcontents}
\@makeschapterhead{\contentsname}\@afterheading
\@mkboth{\MakeUppercase\contentsname}{\MakeUppercase\contentsname}
\@starttoc{toc}}
%------------------------------------------------------------------------------%
\def\title#1{\gdef\@title{#1}\hypersetup{pdftitle={#1}}}
\def\@title{\@latex@error{No title given}\@ehc}
\def\titleClass#1{\gdef\@titleClass{#1}}
\def\@titleClass{\@latex@error{No polish title class given}\@ehc}
\def\@author{\@latex@error{No author given}\@ehc}
\def\supervisor#1{\gdef\@supervisor{#1}}
\def\@supervisor{\@latex@error{No supervisor given}\@ehc}
\def\@discipline{\@latex@error{No discipline given}\@ehc}
%------------------------------------------------------------------------------%
\def\@university{Politechnika Warszawska}
\def\@faculty{Wydzia{\l} Matematyki i Nauk Informacyjnych}
\def\@authorcap{Autorzy}
\def\@supervisorcap{Prowadz{\k a}cy przedmiot}
\def\@location{Warszawa}
\def\@titlepagecap{Strona tytu{\l}owa}
%------------------------------------------------------------------------------%
\def\@definition{Definicja}
\def\@theorem{Twierdzenie}
\def\@lemma{Lemat}
\def\@proposition{Stwierdzenie}
\def\@corollary{Wniosek}
\def\@remark{Uwaga}
\def\@example{Przyk{\l}ad}
%------------------------------------------------------------------------------%
\newtheoremstyle{thm}{3ex}{3ex}{\itshape}{}{\bfseries}{.}{.5em}{#1 #2\thmnote{ (#3)}}
\newtheoremstyle{rem}{3ex}{3ex}{}{}{\bfseries}{.}{.5em}{#1 #2\thmnote{ (#3)}}
\theoremstyle{thm}
\newtheorem{definition}{\@definition}[chapter]
\newtheorem{theorem}{\@theorem}[chapter]
\newtheorem{lemma}[theorem]{\@lemma}
\newtheorem{proposition}[theorem]{\@proposition}
\newtheorem{corollary}[theorem]{\@corollary}
\theoremstyle{rem}
\newtheorem{remark}{\@remark}[chapter]
\newtheorem{example}{\@example}[chapter]
\renewcommand{\qedsymbol}{\ensuremath{\blacksquare}}
%------------------------------------------------------------------------------%

\renewcommand{\maketitle}{
\pdfbookmark[0]{\@titlepagecap}{titlepage}
\begin{titlepage}
\begin{center}
  \begin{minipage}{25mm}
    \includegraphics[scale=0.2]{./Pictures/pw}
  \end{minipage}
  \hfill
  \begin{minipage}{100mm}
    \begin{center}
      \textsc{\Large\@university\\\vspace{1mm}\large\@faculty}
    \end{center}
  \end{minipage}
  \hfill
  \begin{minipage}{25mm}
    \begin{flushright}
      \includegraphics[scale=0.75]{./Pictures/mini}
    \end{flushright}
  \end{minipage}\par
  \vspace{30mm}
   \vfill
  \begin{spacing}{1.3}
  	\textsc{\large Nazwa przedmiotu:\\
     \vspace{1mm}
    \Large\MakeUppercase{\@titleClass}}\par
  	
  \end{spacing}
   \vspace{20mm}
  \begin{spacing}{1.3}
  \textsc{\large Tytu{\l} projektu:\\
     \vspace{1mm}
    \Large\MakeUppercase{\@title}}\par
  \end{spacing}
  \vfill
\end{center}
\hspace{.5\textwidth}
\begin{minipage}[t][70mm]{.5\textwidth}
  \textsc{\large\@authorcap:\\\@author}\par
  \vspace{7mm}
  \textsc{\large\@supervisorcap:\\\@supervisor}\par
\end{minipage}
\begin{center}
  \textsc{\large\@location, \today}
\end{center}
\vspace{5mm}
\clearpage
\thispagestyle{empty}

\end{titlepage}}


\endinput