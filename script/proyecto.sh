#!/bin/bash

function run {
cd ..
    dotnet watch run --project MoogleServer
}

function report {
cd ../informe
pdflatex "informe.tex"
}

function slides {
cd ../presentacion
pdflatex "presentacion.tex"
}

function show_report {
cd ../informe
if [ ! -f "informe.pdf" ]; then
	report
fi
echo "Escriba un visualizador entre ( linux-gnu o darwin ) o presione enter para usar uno por defecto"
read visualizador
if   [[ "$OSTYPE" == "linux-gnu"* ]]; then
	xdg-open "informe.pdf"
elif [[ "$OSTYPE" == "darwin"* ]]; then
	open "informe.pdf"
else
	start "informe.pdf"
fi

}

function show_slides {
cd ../presentacion
if [ ! -f "presentacion.pdf" ]; then
	slides
fi
echo "Escriba un visualizador entre ( linux-gnu o darwin ) o presione enter para usar uno por defecto"
read visualizador
if   [[ "$OSTYPE" == "linux-gnu"* ]]; then
	xdg-open "presentacion.pdf"
elif [[ "$OSTYPE" == "darwin"* ]]; then
	open "presentacion.pdf"
else
	start "presentacion.pdf"
fi

}

function clean {
GLOBIGNORE=informe.pdf:informe.tex:presentacion.pdf:presentacion.tex:imagenes
    cd ../informe
    rm -v *
    cd ../presentacion
    rm -v *
}


echo "Ingrese uno de los siguientes comandos"
echo " "
echo "run :Compilar y jecutar el proyecto"
echo "report :Compilar el informe del proyecto y generar su pdf"
echo "slides :Compilar la presentacion del proyecto y generar su pdf"
echo "show_report :Abrir el informe del proyecto en un visualizador, en caso de que no exista el pdf lo genera primero"
echo "show_slides :Abrir la presentacion del proyecto en un visualizador, en caso de que no exista el pdf lo genera primero"
echo "clean : Para borrar los archivos innecesariosque son generados por la compilacion de los .tex"
echo " "
echo "Comando: "

read comando

case "$comando" in
run)
	run
;;
report)
	report
;;
slides)
	slides
;;
show_report)
	show_report
;;
show_slides)
	show_slides
;;
clean)
	clean
;;
*)
	echo "Opción inválida"
esac
