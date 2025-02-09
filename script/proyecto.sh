#!/bin/bash

# Colores para mensajes en consola
VERDE='\033[0;32m'
ROJO='\033[0;31m'
NC='\033[0m' # No Color

# Función para mostrar mensajes de error
mostrar_error() {
	echo -e "${ROJO}Error: $1${NC}"
}

# Función para mostrar mensajes de éxito
mostrar_exito() {
	echo -e "${VERDE}$1${NC}"
}

# Función para verificar si un directorio existe
verificar_directorio() {
	if [ ! -d "$1" ]; then
		mostrar_error "El directorio $1 no existe"
		exit 1
	fi
}

# Ejecutar el proyecto
function run() {
	verificar_directorio "../MoogleServer"
	cd ..
	mostrar_exito "Iniciando el proyecto..."
	dotnet watch run --project MoogleServer
}

# Generar el informe PDF
function report() {
	verificar_directorio "../informe"
	cd ../informe
	mostrar_exito "Generando informe PDF..."
	pdflatex "informe.tex"
}

# Generar las diapositivas PDF
function slides() {
	verificar_directorio "../presentacion"
	cd ../presentacion
	mostrar_exito "Generando presentación PDF..."
	pdflatex "presentacion.tex"
}

# Función genérica para abrir PDF
function abrir_pdf() {
	local archivo="$1"
	if [[ "$OSTYPE" == "linux-gnu"* ]]; then
		xdg-open "$archivo"
	elif [[ "$OSTYPE" == "darwin"* ]]; then
		open "$archivo"
	else
		start "$archivo"
	fi
}

# Mostrar el informe
function show_report() {
	verificar_directorio "../informe"
	cd ../informe
	if [ ! -f "informe.pdf" ]; then
		report
	fi
	mostrar_exito "Abriendo informe PDF..."
	abrir_pdf "informe.pdf"
}

# Mostrar las diapositivas
function show_slides() {
	verificar_directorio "../presentacion"
	cd ../presentacion
	if [ ! -f "presentacion.pdf" ]; then
		slides
	fi
	mostrar_exito "Abriendo presentación PDF..."
	abrir_pdf "presentacion.pdf"
}

# Limpiar archivos temporales
function clean() {
	verificar_directorio "../informe"
	verificar_directorio "../presentacion"
	GLOBIGNORE=informe.pdf:informe.tex:presentacion.pdf:presentacion.tex:imagenes
	cd ../informe
	mostrar_exito "Limpiando archivos temporales del informe..."
	rm -v *
	cd ../presentacion
	mostrar_exito "Limpiando archivos temporales de la presentación..."
	rm -v *
}

# Mostrar menú de opciones
function mostrar_menu() {
	echo -e "\e[36m==================================\e[0m"
	echo -e "\e[1;33m      GESTOR DE PROYECTO\e[0m"
	echo -e "\e[36m==================================\e[0m"
	echo -e "\e[1;32mComandos disponibles:\e[0m"
	echo -e "\e[36m--------------------------------\e[0m"
	echo -e "\e[1;34mrun         \e[0m: Compilar y ejecutar el proyecto"
	echo -e "\e[1;34mreport      \e[0m: Generar PDF del informe"
	echo -e "\e[1;34mslides      \e[0m: Generar PDF de la presentación"
	echo -e "\e[1;34mshow_report \e[0m: Visualizar el informe PDF"
	echo -e "\e[1;34mshow_slides \e[0m: Visualizar la presentación PDF"
	echo -e "\e[1;34mclean       \e[0m: Limpiar archivos temporales"
	echo -e "\e[36m--------------------------------\e[0m"
	echo -e -n "\e[1;33mIngrese un comando: \e[0m"
}

# Menú principal
mostrar_menu
read comando

case "$comando" in
	run)         run ;;
	report)      report ;;
	slides)      slides ;;
	show_report) show_report ;;
	show_slides) show_slides ;;
	clean)       clean ;;
	*)          mostrar_error "Comando inválido" ;;
esac
