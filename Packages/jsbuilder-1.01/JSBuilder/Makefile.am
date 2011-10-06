
EXTRA_DIST =  

# Warning: This is an automatically generated file, do not edit!

if ENABLE_DEBUG_X86
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize- -debug "-define:DEBUG"
ASSEMBLY = ../../Binary/Debug/jsbuilder.exe
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = exe
PROJECT_REFERENCES = 
BUILD_DIR = ../../Binary/Debug

JSBUILDER_EXE_MDB_SOURCE=../../Binary/Debug/jsbuilder.exe.mdb
JSBUILDER_EXE_MDB=$(BUILD_DIR)/jsbuilder.exe.mdb
TOOLBOX_DLL_SOURCE=../Lib/Toolbox.dll

endif

if ENABLE_RELEASE_X86
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize-
ASSEMBLY = ../../Binary/Release/jsbuilder.exe
ASSEMBLY_MDB = 
COMPILE_TARGET = exe
PROJECT_REFERENCES = 
BUILD_DIR = ../../Binary/Release

JSBUILDER_EXE_MDB=
TOOLBOX_DLL_SOURCE=../Lib/Toolbox.dll

endif

AL=al2
SATELLITE_ASSEMBLY_NAME=$(notdir $(basename $(ASSEMBLY))).resources.dll

PROGRAMFILES = \
	$(JSBUILDER_EXE_MDB) \
	$(TOOLBOX_DLL)  

BINARIES = \
	$(JSBUILDER)  


RESGEN=resgen2
	
all: $(ASSEMBLY) $(PROGRAMFILES) $(BINARIES) 

FILES = \
	Main.cs \
	AssemblyInfo.cs \
	Solution.cs \
	Class.cs \
	Constructor.cs \
	Project.cs \
	Js.cs 

DATA_FILES = 

RESOURCES = 

EXTRAS = \
	jsbuilder.in 

REFERENCES =  \
	System \
	System.Xml

DLL_REFERENCES =  \
	../Lib/Toolbox.dll

CLEANFILES = $(PROGRAMFILES) $(BINARIES) 

include $(top_srcdir)/Makefile.include

TOOLBOX_DLL = $(BUILD_DIR)/Toolbox.dll
JSBUILDER = $(BUILD_DIR)/jsbuilder

$(eval $(call emit-deploy-target,TOOLBOX_DLL))
$(eval $(call emit-deploy-wrapper,JSBUILDER,jsbuilder,x))


$(eval $(call emit_resgen_targets))
$(build_xamlg_list): %.xaml.g.cs: %.xaml
	xamlg '$<'

$(ASSEMBLY_MDB): $(ASSEMBLY)

$(ASSEMBLY): $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES) $(build_xamlg_list) $(build_satellite_assembly_list)
	mkdir -p $(shell dirname $(ASSEMBLY))
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)
